using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Xml;

public class InteractionTrigger : MonoBehaviour
{
    public UnityEvent myOnTriggerEvent;
    public bool myPressToInteract;

    public TextAsset myConversationFile;
    private bool myIsConversation;
    List<DialogMessage> myConversation;

    // Use this for initialization
    void Start()
    {
        if (myConversationFile != null)
        {
            myIsConversation = true;
            myConversation = new List<DialogMessage>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(myConversationFile.text);
            XmlNodeList lineList = xmlDoc.GetElementsByTagName("line");
            foreach (XmlNode line in lineList)
            {
                DialogMessage message = new DialogMessage();
                if (line.Attributes.GetNamedItem("speaker").Value != "")
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
        }
        else
        {
            myIsConversation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag!="Player")
        {
            return;
        }
        if(myPressToInteract == true)
        {
            bool pressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
            if(pressed == true)
            {
                if(myIsConversation == true)
                {
                    StartConversation();
                }
                myOnTriggerEvent.Invoke();
            }
        }
        else
        {
            if (myIsConversation == true)
            {
                StartConversation();
            }
            myOnTriggerEvent.Invoke();
        }
    }


    void StartConversation()
    {
        DialogEvent dialogEvent = new DialogEvent();

        dialogEvent.myDialogMessages = new List<DialogMessage>();

        for (int i = 0; i < myConversation.Count; i++)
        {
            dialogEvent.myDialogMessages.Add(myConversation[i]);
        }

        EventManager.TriggerEvent(dialogEvent);
    }
}
