using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour {


    private static CoinManager coinManager;

    public GameObject myCoinPrefab;
    public int myObjectPoolSize = 100;


    List<GameObject> myCoinObjects;

    public static CoinManager instance
    {
        get
        {
            if (!coinManager)
            {
                coinManager = FindObjectOfType(typeof(CoinManager)) as CoinManager;

                if (!coinManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    coinManager.Init();
                }
            }

            return coinManager;
        }
    }

    void Start()
    {
        CoinManager init = CoinManager.instance;
    }
        void Init()
    {
        myCoinObjects = new List<GameObject>();

        for (int i = 0; i < myObjectPoolSize; i++)
        {
            GameObject newGO = (GameObject)Instantiate(myCoinPrefab);
            newGO.SetActive(false);
            myCoinObjects.Add(newGO);
        }
        EventManager.StartListening(eEvent.SPAWN_COIN, HandleSpawnCoinEvent);
    }

    void HandleSpawnCoinEvent(BaseEvent anEvent)
    {
        CoinEvent coinEvent = (CoinEvent)anEvent;

        for (int i = 0; i < myCoinObjects.Count; i++)
        {
            if (myCoinObjects[i].activeSelf == false)
            {
                myCoinObjects[i].transform.position = coinEvent.myPosition;
                myCoinObjects[i].GetComponent<CoinScript>().myGoldValue = coinEvent.myGoldAmount;
                myCoinObjects[i].SetActive(true);
                return;
            }
        }
    }
    
}
