using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

    public int myHealValue;
    GameObject myTarget;
    Rigidbody myRigidBody;
    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
        
        if (myTarget == null)
        {
            myTarget = FindTarget();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 movementVector = myTarget.transform.position - transform.position;
        float speed = 20f / movementVector.magnitude;
        myRigidBody.MovePosition(transform.position + (movementVector.normalized * speed * Time.deltaTime));
    }

    GameObject FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if ((player.transform.position - transform.position).magnitude < 5 && player.GetComponent<CharacterStats>().myIsDead == false)
            {
                return player;
            }
        }

        return null;
    }

    void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.tag == "Player")
        {
            aCollision.gameObject.GetComponent<CharacterStats>().Heal(myHealValue);
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider aCollisionObject)
    {
        if (aCollisionObject.gameObject.tag == "Player")
        {
            aCollisionObject.gameObject.GetComponent<CharacterStats>().Heal(myHealValue);
            gameObject.SetActive(false);
        }
    }
}
