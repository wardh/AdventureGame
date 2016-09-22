using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{

    public int myGoldValue;
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
            myTarget = FindTarget();
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
        GameObject player = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        GameObject closestTarget = null;

        if ((player.transform.position - transform.position).magnitude < 5 && player.GetComponent<CharacterStats>().myIsDead == false)
        {
            closestTarget = player;
        }


        return closestTarget;
    }

    void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.tag == "Player")
        {
            //aCollision.gameObject.GetComponent<CharacterStats>().AddGold(myGoldValue);
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider aCollisionObject)
    {
        if (aCollisionObject.gameObject.tag == "Player")
        {
            //aCollisionObject.gameObject.GetComponent<CharacterStats>().AddGold(myGoldValue);
            gameObject.SetActive(false);
        }
    }
}