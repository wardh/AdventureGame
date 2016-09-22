using UnityEngine;
using System.Collections;

public class BanditBehaviour : MonoBehaviour
{
    NavMeshAgent myAgent;
    GameObject myPlayerObject;
    public float myAggroRange;
    private float myStartingAngular;
    private float myAttackTimer;
    private bool myIsAttacking;
    private Vector3 myCircleDirection;
    private float myStartSpeed;
    // Use this for initialization
    void Start()
    {
        myPlayerObject = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        myAgent = GetComponent<NavMeshAgent>();
        myIsAttacking = false;
        myAttackTimer = 1;
        myStartingAngular = myAgent.angularSpeed;
        myStartSpeed = myAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if ((myPlayerObject.transform.position - transform.position).magnitude <= myAggroRange)
        {
            if (myAttackTimer > 0)
            {
                myAttackTimer -= Time.deltaTime;
                if (myAttackTimer <= 0)
                {
                    myAttackTimer = Random.Range(1f, 3f);
                    myIsAttacking = !myIsAttacking;
                    if (myIsAttacking == false)
                    {
                        myAgent.ResetPath();
                        myAgent.angularSpeed = 0;
                        myAgent.speed = myStartSpeed;
                        int rand = Random.Range(1, 3);
                        if (rand == 1)
                        {
                            myCircleDirection = transform.right;
                        }
                        else
                        {
                            myCircleDirection = transform.right * -1f;
                        }
                    }
                    else
                    {
                        myAgent.angularSpeed = myStartingAngular;
                        myAgent.speed = myStartSpeed + 1;
                    }
                }
            }
            if (myIsAttacking == false)
            {
                myAgent.Move(myCircleDirection* myAgent.speed * Time.deltaTime);
                Vector3 dirToPlayer = myPlayerObject.transform.position - transform.position;
                dirToPlayer.y = 0;
                dirToPlayer.Normalize();
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToPlayer, Vector3.up), Time.deltaTime);
            }
            else
            {
                myAgent.SetDestination(myPlayerObject.transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myAggroRange);
    }
}
