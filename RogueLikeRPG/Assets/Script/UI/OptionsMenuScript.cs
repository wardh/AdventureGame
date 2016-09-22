using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public GameObject myOptionsPanel;
    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(eEvent.OPTIONS_MENU, HandleOptionsMenuEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HandleOptionsMenuEvent(BaseEvent anEvent)
    {
        OptionsMenuEvent optionsEvent = (OptionsMenuEvent)anEvent;

        if(optionsEvent.myOpenOptions == true)
        {
            OpenOptionsMenu();
        }
        else
        {
            CloseOptionsMenu();
        }
    }

    public void CloseOptionsMenu()
    {
        LockPlayerMovementEvent lockPlayerEvent = new LockPlayerMovementEvent();
        lockPlayerEvent.myLockPlayer = false;
        EventManager.TriggerEvent(lockPlayerEvent);

        myOptionsPanel.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        LockPlayerMovementEvent lockPlayerEvent = new LockPlayerMovementEvent();
        lockPlayerEvent.myLockPlayer = true;
        EventManager.TriggerEvent(lockPlayerEvent);

        myOptionsPanel.SetActive(true);
    }
}
