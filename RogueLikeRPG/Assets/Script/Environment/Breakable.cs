using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour
{
    public GameObject myParticleGameObject;
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
        if (other.gameObject.GetComponent<WeaponComponent>() != null && other.gameObject.GetComponent<WeaponComponent>().myIsActive == true)
        {
            Instantiate(myParticleGameObject, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.up));
            Destroy(gameObject);
        }
    }
}
