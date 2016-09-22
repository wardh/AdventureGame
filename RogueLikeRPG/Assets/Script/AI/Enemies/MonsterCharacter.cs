using UnityEngine;
using System.Collections;

public class MonsterCharacter : MonoBehaviour
{
    private CharacterStats myStats;
    private NavMeshAgent myAgent;
    public float myPushBackResistance;
    private Vector3 myDamageVelocity;
    public GameObject myDeathParticleSystem;
    public GameObject myDamageParticleSystem;

    private Vector3 myLastAttackOrigin;

    void Awake()
    {
        myStats = GetComponent<CharacterStats>();
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = myStats.myMovementSpeed;
        myAgent.angularSpeed = myStats.myTurnSpeed;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myAgent.Move(myDamageVelocity * Time.deltaTime);
        myDamageVelocity -= myDamageVelocity * Time.deltaTime * 10f;

    }

    public void TakeDamage(DamageStruct aDmgStruct)
    {
        myDamageVelocity = (transform.position - aDmgStruct.myOrigin).normalized * Mathf.Max(aDmgStruct.myPushBackStrength - myPushBackResistance, 0);
        Instantiate(myDamageParticleSystem, transform.position + new Vector3(0, myAgent.height, 0), Quaternion.LookRotation((transform.position - myLastAttackOrigin).normalized, Vector3.up));
        myLastAttackOrigin = aDmgStruct.myOrigin;
    }

    public void Death()
    {
        myAgent.enabled = false;
        DamageOnTouch damageOnTouch = GetComponent<DamageOnTouch>();
        if (damageOnTouch != null)
        {
            Destroy(damageOnTouch);
        }
        Instantiate(myDeathParticleSystem, transform.position + new Vector3(0, myAgent.height, 0), Quaternion.LookRotation((transform.position - myLastAttackOrigin).normalized, Vector3.up));
        Destroy(gameObject);
    }
}
