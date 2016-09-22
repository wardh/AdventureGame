using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float myTime;
    // Use this  for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myTime -= Time.deltaTime;
        if(myTime <=0)
        {
            Destroy(gameObject); 
        }
    }
}
