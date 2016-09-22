using UnityEngine;
using System.Collections;

public class AttackWhenClose : MonoBehaviour
{
    public float myAttackRange;
    public float myAttackCooldown;
    private float myAttackDeltaTime;
    private GameObject myPlayerObject;
    private WeaponComponent myWeapon;
    private Animator myAnimator;
    // Use this for initialization
    void Start()
    {
        myPlayerObject = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        myAttackDeltaTime = myAttackCooldown;
        myWeapon = GetComponentInChildren<WeaponComponent>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myAttackDeltaTime > 0)
        {
            myAttackDeltaTime -= Time.deltaTime;
        }

        if ((myPlayerObject.transform.position - transform.position).magnitude <= myAttackRange && myAttackDeltaTime <= 0)
        {
            myAttackDeltaTime = myAttackCooldown;
            myAnimator.SetBool("IsAttacking", true);
            myWeapon.myIsActive = true;
        }

    }

    public void StopAttacking()
    {
        myWeapon.myIsActive = false;
        myAnimator.SetBool("IsAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myAttackRange);
    }
}
