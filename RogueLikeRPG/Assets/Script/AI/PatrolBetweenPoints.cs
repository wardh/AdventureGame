using UnityEngine;
using System.Collections.Generic;

public class PatrolBetweenPoints : MonoBehaviour
{

    // Use this for initialization
    public List<Vector3> myPatrolPoints;
    public float myMovementSpeed;
    private int myCurrentIndex;
    void Start()
    {
        myPatrolPoints.Add(transform.position);
        myCurrentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if ((transform.position - myPatrolPoints[myCurrentIndex]).magnitude <= 1)
        {
            myCurrentIndex++;
            if (myCurrentIndex >= myPatrolPoints.Count)
            {
                myCurrentIndex = 0;
            }
        }

        Vector3 direction = (myPatrolPoints[myCurrentIndex] - transform.position);
        direction.y = 0;
        direction.Normalize();
        GetComponent<Rigidbody>().MovePosition(transform.position + (direction * myMovementSpeed * Time.deltaTime));
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 10*Time.deltaTime));
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        foreach (Vector3 position in myPatrolPoints)
        {
            Gizmos.DrawSphere(position, 1);
        }
    }
}
