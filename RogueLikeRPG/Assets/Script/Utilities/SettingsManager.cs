using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {


    private static SettingsManager settingsManager;
    public bool myInvertedY;
    public static SettingsManager instance
    {
        get
        {
            if (!settingsManager)
            {
                settingsManager = FindObjectOfType(typeof(SettingsManager)) as SettingsManager;

                if (!settingsManager)
                {
                    Debug.LogError("There needs to be one active progressionManager script on a GameObject in your scene.");
                }
                else
                {
                    settingsManager.Init();
                }
            }

            return settingsManager;
        }
    }

    void Init()
    {
    }

    public void SetInvertedOn()
    {
        myInvertedY = true;
    }

    public void SetInvertedOff()
    {
        myInvertedY = false;

    }

    public void ToggleInverted()
    {
        myInvertedY = !myInvertedY;
    }

    public bool GetIsInverted()
    {
        return myInvertedY;
    }
}
