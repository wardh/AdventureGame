using UnityEngine;

using System.Collections.Generic;
using System.Xml;

public class ConversationScript : MonoBehaviour {

    public TextAsset myConversationFile;
    List<DialogMessage> myConversation;
    public GameObject myDialogIconGameObject;
    bool myPlayerIsLocked;
    public bool myUseIcon;
    float myLockCoolDown;
    public float myRadius;
	void Start ()
    {
        myPlayerIsLocked = false;
        myConversation = new List<DialogMessage>();
        EventManager.StartListening(eEvent.LOCK_PLAYER_MOVEMENT, HandleLockPlayerMovementEvent);
        myLockCoolDown = 0;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(myConversationFile.text);
        XmlNodeList lineList = xmlDoc.GetElementsByTagName("line");
        foreach (XmlNode line in lineList)
        {
            DialogMessage message = new DialogMessage();
            if(line.Attributes.GetNamedItem("speaker").Value != "")
            {
                message.myNameExists = true;
                message.myName = line.Attributes.GetNamedItem("speaker").Value;
            }
            else
            {
                message.myNameExists = false;
            }

            message.myDialogText = line.Attributes.GetNamedItem("text").Value;
            myConversation.Add(message);
        }


        myDialogIconGameObject = Instantiate(myDialogIconGameObject);
        myDialogIconGameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myDialogIconGameObject.transform.position = transform.position + ((Vector3.up*2) + new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0));

        if (myPlayerIsLocked == true)
        {
            return;
        }
        if(myLockCoolDown >=0)
        {
            myLockCoolDown -= Time.deltaTime;
            return;
        }
        GameObject player = PollingStation.instance.GetGameObject(ePollingStationGameObjects.PLAYEROBJECT);
        if ((transform.position - player.transform.position).magnitude <= myRadius)
        {
            if(myDialogIconGameObject.activeSelf == false && myUseIcon == true)
            {
                myDialogIconGameObject.SetActive(true);
            }
           
            bool startConv = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
            if (startConv == true)
            {
                //myDialogIconGameObject.SetActive(false);

                DialogEvent dialogEvent = new DialogEvent();

                dialogEvent.myDialogMessages = new List<DialogMessage>();

                for (int i = 0; i < myConversation.Count; i++)
                {
                    dialogEvent.myDialogMessages.Add(myConversation[i]);
                }

                EventManager.TriggerEvent(dialogEvent);
                
            }
        }
        else
        {
            if (myDialogIconGameObject.activeSelf == true && myUseIcon == true)
            {
                myDialogIconGameObject.SetActive(false);
            }
        }
	}


    void HandleLockPlayerMovementEvent(BaseEvent anEvent)
    {
        LockPlayerMovementEvent lockEvent = (LockPlayerMovementEvent)anEvent;
        myPlayerIsLocked = lockEvent.myLockPlayer;
        if(myPlayerIsLocked == false)
        {
            myLockCoolDown = 0.3f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, myRadius);
    }
}
