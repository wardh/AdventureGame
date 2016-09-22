using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationUIScript : MonoBehaviour {

    public Text myDialogText;
    public Text myNameText;
    public GameObject myNamePanel;
    public GameObject myDialogTextPanel;

    private List<DialogMessage> myCurrentMessages;
    bool myRunDialog;
    float myPressCooldown;
    // Use this for initialization
    void Start ()
    {
        EventManager.StartListening(eEvent.SHOW_DIALOG, HandleDialogEvent);
        myNamePanel.SetActive(false);
        myDialogTextPanel.SetActive(false);
        myCurrentMessages = new List<DialogMessage>();
        myRunDialog = false;
        myPressCooldown = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(myRunDialog == false)
        {
            return;
        }
        if(myPressCooldown >0)
        {
            myPressCooldown -= Time.deltaTime;
            return;
        }
        bool next = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
        if (next == true)
        {
            myCurrentMessages.RemoveAt(0);
            if (myCurrentMessages.Count > 0)
            {
                myRunDialog = true;
                myDialogTextPanel.SetActive(true);
                myDialogText.text = myCurrentMessages[0].myDialogText;
                if (myCurrentMessages[0].myNameExists == true)
                {
                    myNamePanel.SetActive(true);
                    myNameText.text = myCurrentMessages[0].myName;
                }
                myPressCooldown = 0.5f;
            }
            else
            {
                myRunDialog = false;
                myNamePanel.SetActive(false);
                myDialogTextPanel.SetActive(false);
                LockPlayerMovementEvent lockEvent = new LockPlayerMovementEvent();
                lockEvent.myLockPlayer = false;
                EventManager.TriggerEvent(lockEvent);
            }
        }
    }

    void HandleDialogEvent(BaseEvent anEvent)
    {
        DialogEvent dialogEvent = (DialogEvent)anEvent;
        myCurrentMessages = dialogEvent.myDialogMessages;
        if(myCurrentMessages.Count >0)
        {
            myRunDialog = true;
            myDialogTextPanel.SetActive(true);
                myPressCooldown = 0.5f;
            myDialogText.text = myCurrentMessages[0].myDialogText;
            if (myCurrentMessages[0].myNameExists == true)
            {
                myNamePanel.SetActive(true);
                myNameText.text = myCurrentMessages[0].myName;
                LockPlayerMovementEvent lockEvent = new LockPlayerMovementEvent();
                lockEvent.myLockPlayer = true;
                EventManager.TriggerEvent(lockEvent);
            }
        }
    }

}
