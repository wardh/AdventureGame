using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public float myDistanceFromPlayer;
    private Transform myPlayerTransform;
    public float myCameraSpeed = 1;
    // Use this for initialization
    void Start()
    {
        myPlayerTransform = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, (transform.forward * -1f) * myDistanceFromPlayer + myPlayerTransform.position, myCameraSpeed* Time.deltaTime);
    }
}
