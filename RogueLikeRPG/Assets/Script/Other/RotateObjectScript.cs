using UnityEngine;
using System.Collections;

public class RotateObjectScript : MonoBehaviour
{

    // Use this for initialization
    public Vector3 myWorldRotationPerSecond;
    public Vector3 myLocalRotationPerSecond;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        transform.Rotate(myWorldRotationPerSecond * Time.deltaTime,Space.World);
        transform.Rotate(myLocalRotationPerSecond * Time.deltaTime,Space.Self);
    }
}
