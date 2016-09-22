using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    public GameObject myPlayer;

    private static PlayerManager playerManager;
    public bool myPlayerHasGlider;
    public static PlayerManager instance
    {
        get
        {
            if (!playerManager)
            {
                playerManager = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;

                if (!playerManager)
                {
                    Debug.LogError("There needs to be one active progressionManager script on a GameObject in your scene.");
                }
                else
                {
                    playerManager.Init();
                }
            }

            return playerManager;
        }
    }

    void Init()
    {
        
    }

    public GameObject GetPlayerObject()
    {
        return myPlayer;
    }

    public void SetPlayerObject(GameObject aPlayer)
    {
        myPlayer = aPlayer;
    }

    public void GivePlayerGlider()
    {
        myPlayerHasGlider = true;
        ProgressionManager.instance.SetValue("HasGlider", true);
    }

}
