using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{

    public GameObject myFilledHeart;
    public GameObject myEmptyHeart;
    private CharacterStats myPlayerStats;

    List<GameObject> myFilledHearts;
    List<GameObject> myEmptyHearts;
    // Use this for initialization
    void Start()
    {
        myPlayerStats = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT).GetComponent<CharacterStats>();
        myFilledHearts = new List<GameObject>();
        myEmptyHearts = new List<GameObject>();
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        for (int i = 0; i < myPlayerStats.myMaxHealth; i++)
        {
            GameObject newFilledHeart = Instantiate(myFilledHeart);
            newFilledHeart.transform.SetParent(transform);
            ((RectTransform)newFilledHeart.transform).localPosition = new Vector3(16 + (38 * i), -32);
            myFilledHearts.Add(newFilledHeart);

            GameObject newEmptyHeart = Instantiate(myEmptyHeart);
            newEmptyHeart.transform.SetParent(transform);
            ((RectTransform)newEmptyHeart.transform).localPosition = new Vector3(16 + (38 * i), -32);
            myEmptyHearts.Add(newEmptyHeart);
        }


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < myPlayerStats.myMaxHealth; i++)
        {
            if (i < myPlayerStats.myCurrentHealth)
            {
                myFilledHearts[i].SetActive(true);
                myEmptyHearts[i].SetActive(false);
            }
            else
            {
                myFilledHearts[i].SetActive(false);
                myEmptyHearts[i].SetActive(true);
            }
        }
    }
}
