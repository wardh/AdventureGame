using UnityEngine;
using System.Collections;

public class MoveTowardsPlayerBehaviour : MonoBehaviour
{
    NavMeshAgent myAgent;
    GameObject myPlayerObject;
    public float myAggroRange;

    // Use this for initialization
    void Start()
    {
        myPlayerObject = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if((myPlayerObject.transform.position - transform.position).magnitude <= myAggroRange)
        {
            myAgent.SetDestination(myPlayerObject.transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myAggroRange);
    }
}
