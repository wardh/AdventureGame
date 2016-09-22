using UnityEngine;
using System.Collections;

public class WindSliceScript : MonoBehaviour
{

    // Use this for initialization
    public float mySpeed;
    float myRotateSpeed;
    float myLifeTime;
    TrailRenderer myTrailRenderer;
    bool myHasShutDownTrail;
    void Start()
    {
        myRotateSpeed = Random.Range(-360, 360);
        myTrailRenderer = GetComponentInChildren<TrailRenderer>();
        myLifeTime = Random.Range(6, 15);
        myHasShutDownTrail = false;
    }

    // Update is called once per frame
    void Update()
    {
        myLifeTime -= Time.deltaTime;
        if (myHasShutDownTrail == false && myLifeTime < 2)
        {
            myHasShutDownTrail = true;
            myTrailRenderer.gameObject.transform.parent = null;
            GameObject.Destroy(myTrailRenderer.gameObject, 2);
            GameObject.Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * mySpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, myRotateSpeed * Time.deltaTime, 0));
    }
}
