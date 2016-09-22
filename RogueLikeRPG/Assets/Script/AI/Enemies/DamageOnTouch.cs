using UnityEngine;
using System.Collections;

public class DamageOnTouch : MonoBehaviour {

    // Use this for initialization
    public int myDamage;
    public float myPushBackStrength;
    float myDamageCooldownDeltaTime;

	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(myDamageCooldownDeltaTime >0)
        {
            myDamageCooldownDeltaTime -= Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        HandleDamageCollision(other);
    }

    void OnTriggerStay(Collider other)
    {
        HandleDamageCollision(other);
    }

    void HandleDamageCollision(Collider other)
    {
        if (myDamageCooldownDeltaTime > 0)
        {
            return;
        }
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "Player")
        {
            DamageStruct dmgStruct;
            dmgStruct.myAmountOfDamage = myDamage;
            dmgStruct.myPushBackStrength = myPushBackStrength;
            dmgStruct.myOrigin = transform.position;
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(dmgStruct);
            myDamageCooldownDeltaTime = 1;
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(dmgStruct);
            }
        }
    }

}
