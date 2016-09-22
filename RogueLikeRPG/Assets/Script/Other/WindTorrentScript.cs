using UnityEngine;
using System.Collections;

public class WindTorrentScript : MonoBehaviour
{


    public GameObject myWindSliceGameObject;
    public float myRiseSpeed;
    public float mySliceSpawnRate;

    private Vector3 myExtents;
    private float mySpawnDeltaTime;
    // Use this for initialization
    void Start()
    {
        mySpawnDeltaTime = mySliceSpawnRate;
        myExtents = GetComponent<Collider>().bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
        mySpawnDeltaTime -= Time.deltaTime;
        if (mySpawnDeltaTime <= 0)
        {
            Instantiate(myWindSliceGameObject, transform.position + new Vector3(Random.Range(-myExtents.x, myExtents.x), -myExtents.y, Random.Range(-myExtents.z, myExtents.z)), new Quaternion());
            mySpawnDeltaTime = mySliceSpawnRate;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {

            other.attachedRigidbody.AddForce(Vector3.up * myRiseSpeed);

        }
    }
}
