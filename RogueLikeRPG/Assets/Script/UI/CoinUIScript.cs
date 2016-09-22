using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CoinUIScript : MonoBehaviour
{
    private CharacterStats myPlayerStats;
    Text myAmountOfCoins;
    // Use this for initialization
    void Start()
    {
        myPlayerStats = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT).GetComponent<CharacterStats>();

        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        myAmountOfCoins = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myAmountOfCoins.text = myPlayerStats.myGold.ToString();
    }
}
