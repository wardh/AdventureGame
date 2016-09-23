using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private CharacterStats myStats;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private WeaponComponent myWeapon;

    private Vector3 myMovementThisFrame;
    private Vector3 myAttackMovement;
    private float myAttackLockDeltaTime;
    public float myRollSpeed;
    private Vector3 myRollDirection;
    private Vector3 myDamageVelocity;
    private bool myIsRolling;
    private float myRollLockDeltaTime;

    private bool myIsLocked;

    private void Awake()
    {
        PollingStation.instance.SetGameObject(ePollingStationGameObjects.PLAYEROBJECT, gameObject);
        myCharacterController = GetComponent<CharacterController>();
        myStats = GetComponent<CharacterStats>();
        myAnimator = GetComponent<Animator>();
        myMovementThisFrame = new Vector3();
        myWeapon = GetComponentInChildren<WeaponComponent>();
        myAttackLockDeltaTime = 0;
        myIsRolling = false;
    }

    // Use this for initialization
    private void Start()
    {
        EventManager.StartListening(eEvent.LOCK_PLAYER_MOVEMENT, HandleLockPlayerMovementEvent);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateInput();
        UpdateMovement();
        UpdateTimers();
    }

    private void UpdateInput()
    {
        if (myIsLocked == false)
        {
            MovementInput();
            AttackInput();
            RollInput();
        }
    }

    private void MovementInput()
    {
        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = Camera.main.transform.forward;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 result = new Vector3();
            result.x = movement.x * cameraRight.z;
            result.z = movement.y * cameraForward.z;
            myMovementThisFrame = result * myStats.myMovementSpeed;
            myAnimator.SetBool("IsWalking", true);
            myAnimator.SetFloat("WalkSpeed", movement.magnitude);
        }
        else
        {
            myAnimator.SetBool("IsWalking", false);
        }
    }

    void AttackInput()
    {
        bool attack = Input.GetKeyDown(KeyCode.Joystick1Button0);
        if (attack && myAnimator.GetBool("IsAttacking") == false)
        {
            myAnimator.SetBool("IsAttacking", true);
            myWeapon.myIsActive = true;
            myAttackLockDeltaTime = 0.10f;
            if (myMovementThisFrame != Vector3.zero)
            {
                myAttackMovement = myMovementThisFrame * 3f;
            }
        }
    }

    void RollInput()
    {
        bool roll = Input.GetKeyDown(KeyCode.Joystick1Button1);
        if (roll && myAnimator.GetBool("IsAttacking") == false && myRollLockDeltaTime <= 0)
        {
            if (myMovementThisFrame != Vector3.zero)
            {
                myAnimator.SetBool("IsRolling", true);
                myRollDirection = myMovementThisFrame.normalized;
                myIsRolling = true;
            }
        }
    }

    void UpdateMovement()
    {
        Vector3 totalMovement = new Vector3();
        if (myAttackLockDeltaTime <= 0 && myIsRolling == false)
        {
            totalMovement += myMovementThisFrame;
        }
        Vector3 gravityVector = new Vector3(0, Physics.gravity.y, 0);
        totalMovement += gravityVector;
        totalMovement += myAttackMovement;
        if (myIsRolling == true)
        {
            totalMovement += myRollDirection * myRollSpeed;
        }

        totalMovement += myDamageVelocity;

        myCharacterController.Move(totalMovement * Time.deltaTime);
        if (myMovementThisFrame != Vector3.zero && myIsRolling == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myMovementThisFrame.normalized), myStats.myTurnSpeed * Time.deltaTime);
        }
        else if (myIsRolling == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myRollDirection.normalized), myStats.myTurnSpeed * Time.deltaTime);

        }
        myAttackMovement *= 50f * Time.deltaTime;
        myDamageVelocity -= myDamageVelocity * Time.deltaTime * 10f;
        myMovementThisFrame = Vector3.zero;
    }

    void UpdateTimers()
    {
        if (myAttackLockDeltaTime > 0)
        {
            myAttackLockDeltaTime -= Time.deltaTime;
        }
        if (myRollLockDeltaTime > 0)
        {
            myRollLockDeltaTime -= Time.deltaTime;
        }
    }

    public void StopAttacking()
    {
        myAnimator.SetBool("IsAttacking", false);
        myWeapon.myIsActive = false;
    }

    public void StopRolling()
    {
        myAnimator.SetBool("IsRolling", false);
        myIsRolling = false;
        myRollLockDeltaTime = 0.1f;
    }

    public void TakeDamage(DamageStruct aDmgStruct)
    {
        myDamageVelocity = (transform.position - aDmgStruct.myOrigin).normalized * aDmgStruct.myPushBackStrength;
    }

    public void Death()
    {

    }

    private void HandleLockPlayerMovementEvent(BaseEvent anEvent)
    {
        LockPlayerMovementEvent lockEvent = (LockPlayerMovementEvent)anEvent;
        myIsLocked = lockEvent.myLockPlayer;
    }

}
