using UnityEngine;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    Dictionary<string, bool> myProgressionData;

    private static ProgressionManager progressionManager;

    public static ProgressionManager instance
    {
        get
        {
            if (!progressionManager)
            {
                progressionManager = FindObjectOfType(typeof(ProgressionManager)) as ProgressionManager;

                if (!progressionManager)
                {
                    Debug.LogError("There needs to be one active progressionManager script on a GameObject in your scene.");
                }
                else
                {
                    progressionManager.Init();
                }
            }

            return progressionManager;
        }
    }

    void Init()
    {
        if (myProgressionData == null)
        {
            myProgressionData = new Dictionary<string, bool>();
        }
    }

    public void SetValue(string aKey, bool aValue)
    {
        myProgressionData[aKey] = aValue;
    }

    public bool GetValue(string aKey)
    {
        if(myProgressionData.ContainsKey(aKey) == true)
        {
            return myProgressionData[aKey];
        }
        else
        {
            myProgressionData[aKey] = false;
            return myProgressionData[aKey];
        }
    }

}
