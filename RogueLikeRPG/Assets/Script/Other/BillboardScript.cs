using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour
{

    // Use this for initialization
    private Transform myCameraTransform;
	void Start()
    {
        myCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation((transform.position- myCameraTransform.position).normalized, Vector3.up);

    }
}
