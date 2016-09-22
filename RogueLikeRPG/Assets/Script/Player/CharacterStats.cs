using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public eFactions myFaction;

    public int myMaxHealth;
    public int myCurrentHealth;
    public int myGold;
    public float myMovementSpeed = 1;
    public float myTurnSpeed = 10;
    public bool myIsDead = false;

    // Use this for initialization
    void Start()
    {
        myCurrentHealth = myMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int anAmount)
    {
        myCurrentHealth -= anAmount;
        if(myCurrentHealth <=0)
        {
            myCurrentHealth = 0;
            myIsDead = true;
            Death();
        }
    }

    public void TakeDamage(DamageStruct someDamage)
    {
        myCurrentHealth -= someDamage.myAmountOfDamage;
        if (myCurrentHealth <= 0)
        {
            myCurrentHealth = 0;
            myIsDead = true;
            Death();
        }
    }

    public void Heal(int anAmount)
    {
        myCurrentHealth += anAmount;
        myCurrentHealth = Mathf.Min(myCurrentHealth, myMaxHealth);
    }

    private void Death()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        MonsterCharacter monsterCharacter = GetComponent<MonsterCharacter>();
        if(playerController != null)
        {
            playerController.Death();
        }
        if (monsterCharacter != null)
        {
            monsterCharacter.Death();
        }
    }

    public void AddGold(int anAmount)
    {
        myGold += anAmount;
    }



}
