using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

class UnityEventBase : UnityEvent <BaseEvent>
{

}


public class EventManager : MonoBehaviour
{

    private Dictionary<eEvent, UnityEventBase> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<eEvent, UnityEventBase>();
        }
    }

    public static void StartListening(eEvent eventName, UnityAction<BaseEvent> listener)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEventBase();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(eEvent eventName, UnityAction<BaseEvent> listener)
    {
        if (eventManager == null) return;
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(BaseEvent someEventData)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(someEventData.myEventType, out thisEvent))
        {
            thisEvent.Invoke(someEventData);
        }
    }
}