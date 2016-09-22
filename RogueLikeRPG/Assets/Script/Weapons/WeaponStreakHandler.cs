using UnityEngine;
using System.Collections;

public class WeaponStreakHandler : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    WeaponComponent myWeaponComponent;
    // Use this for initialization
    void Start()
    {
        myWeaponComponent = GetComponent<WeaponComponent>();
       // myParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(myWeaponComponent.myIsActive == true)
        {
            myParticleSystem.Play();
        }
        if (myWeaponComponent.myIsActive == false)
        {
            myParticleSystem.Stop();
        }
    }
}
