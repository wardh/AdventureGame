using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class RunFunctionOnActivateButtonScript : MonoBehaviour {

    // Use this for initialization
    public UnityEvent myFunctionsOnActivate;
    public float myRadius;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject player = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        if ((transform.position - player.transform.position).magnitude <= myRadius)
        {

            bool activate = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
            if (activate == true)
            {

                myFunctionsOnActivate.Invoke();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, myRadius);
    }
}
