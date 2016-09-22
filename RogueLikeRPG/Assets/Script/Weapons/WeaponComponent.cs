using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour
{
    public bool myIsActive;
    public int myDamage;
    public float myPushBackStrength;
    public eFactions myFaction;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(myIsActive == false)
        {
            return;
        }
        CharacterStats statsComponent = other.gameObject.GetComponent<CharacterStats>();
        if (statsComponent != null)
        {
            DamageStruct dmgStruct = new DamageStruct();
            dmgStruct.myOrigin = transform.root.position;
            dmgStruct.myAmountOfDamage = myDamage;
            dmgStruct.myPushBackStrength = myPushBackStrength;
            if (myFaction == eFactions.PLAYER && statsComponent.myFaction == eFactions.MONSTER)
            {
                statsComponent.TakeDamage(dmgStruct);
                MonsterCharacter monster = other.gameObject.GetComponent<MonsterCharacter>();
                monster.TakeDamage(dmgStruct);
            }
            else if(myFaction == eFactions.NPC && statsComponent.myFaction == eFactions.MONSTER)
            {
                statsComponent.TakeDamage(dmgStruct);
                MonsterCharacter monster = other.gameObject.GetComponent<MonsterCharacter>();
                monster.TakeDamage(dmgStruct);
            }
            else if (myFaction == eFactions.MONSTER && (statsComponent.myFaction == eFactions.NPC || statsComponent.myFaction == eFactions.PLAYER))
            {
                statsComponent.TakeDamage(dmgStruct);
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                player.TakeDamage(dmgStruct);
            }
        }
    }
}
