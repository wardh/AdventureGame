using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour
{


    public GameObject myLeader;
    public float mySchoolSize;
    public float mySpeed;
    private Vector3 myStartPosition;
    Vector3 myLastDirection;

    bool myShouldMove;
    float myMoveTimer;
    // Use this for initialization
    void Start()
    {
        myShouldMove = false;
        myMoveTimer = Random.Range(0, 1);
        myStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myMoveTimer -= Time.deltaTime;

        if (myMoveTimer < 0)
        {
            myShouldMove = !myShouldMove;
            if (myShouldMove == true)
            {
                myMoveTimer = Random.Range(2, 5);
            }
            else
            {
                myMoveTimer = Random.Range(0, 1);
            }
        }
        if (myShouldMove == true)
        {
            if (myLeader == null)
            {
                LeaderMovement();
            }
            else
            {
                FollowMovement();
            }
        }

        if (myShouldMove == true)
        {
            if (myStartPosition.y < (transform.position + (myLastDirection * (myMoveTimer * 0.1f) * mySpeed * Time.deltaTime)).y)
            {
                myLastDirection.y = 0;
                myLastDirection.Normalize();
            }
            transform.position = transform.position + (myLastDirection * (myMoveTimer * 0.1f) * mySpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(myLastDirection, Vector3.up), 0.5f);
        }
        else
        {
            if (myStartPosition.y < (transform.position + (myLastDirection * mySpeed * 0.1f * Time.deltaTime)).y)
            {
                myLastDirection.y = 0;
                myLastDirection.Normalize();
            }
            transform.position = transform.position + (myLastDirection * mySpeed * 0.1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(myLastDirection, Vector3.up), 0.5f);
        }

    }

    void LeaderMovement()
    {
        if ((myStartPosition - transform.position).magnitude > mySchoolSize)
        {
            myLastDirection = (myStartPosition - transform.position).normalized;
        }
        else
        {
            Vector3 randomVector = new Vector3(Random.Range(-10, 10), Random.Range(-2, 2), Random.Range(-10, 10));

            if(myStartPosition.y < randomVector.y + transform.position.y)
            {
                randomVector.y = 0;
            }

            randomVector.Normalize();
            myLastDirection += randomVector * 0.1f;
            myLastDirection.Normalize();
        }
    }

    void FollowMovement()
    {
        Vector3 randomVector = new Vector3(Random.Range(-10, 10), Random.Range(-2, 2), Random.Range(-10, 10));
        randomVector += myLeader.transform.position - transform.position;
        if (myStartPosition.y < randomVector.y + transform.position.y)
        {
            randomVector.y = 0;
        }
        randomVector.Normalize();
        myLastDirection += randomVector * 0.1f;
        myLastDirection.Normalize();
    }

    void OnDrawGizmosSelected()
    {
        if (myLeader == null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, mySchoolSize);
        }
    }
}
