using UnityEngine;
using System.Collections.Generic;


public enum ePollingStationGameObjects
{
    PLAYEROBJECT
}

public class PollingStation : MonoBehaviour
{

    private Dictionary<ePollingStationGameObjects, GameObject> myGameObjects;
    private static PollingStation pollingStation;

    public static PollingStation instance
    {
        get
        {
            if (!pollingStation)
            {
                pollingStation = FindObjectOfType(typeof(PollingStation)) as PollingStation;

                if (!pollingStation)
                {
                    Debug.LogError("There needs to be one active PollingStation script on a GameObject in your scene.");
                }
                else
                {
                    pollingStation.Init();
                }
            }

            return pollingStation;
        }
    }

    void Init()
    {
        myGameObjects = new Dictionary<ePollingStationGameObjects, GameObject>();
    }

    public GameObject GetGameObject(ePollingStationGameObjects aKey)
    {
        if (myGameObjects.ContainsKey(aKey) == true)
        {
            return myGameObjects[aKey];
        }
        else
        {
            Debug.LogError("GameObject isn't set in PollingStation. Key: " + aKey.ToString());

            return null;
        }
    }

    public void SetGameObject(ePollingStationGameObjects aKey, GameObject aGameObject)
    {
        myGameObjects[aKey] = aGameObject;
    }
}
