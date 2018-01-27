using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class GameEvent : UnityEvent<NetworkAction>
{
}

public class EventManager : MonoBehaviour
{
    private Dictionary<string, GameEvent> eventDictionary;

    public static EventManager instance = null;

    // Message queue for moving events from network thread to UI thread
    private static List<NetworkAction> actionQueue = new List<NetworkAction>();
    private static System.Object queueLock = new System.Object();

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            eventDictionary = new Dictionary<string, GameEvent>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        lock (queueLock)
        {
            while (actionQueue.Count > 0)
            {
                NetworkAction action = actionQueue[0];
                TriggerEvent("game", action);
                actionQueue.RemoveAt(0);
            }
        }
    }

    public static void StartListening(string eventName, UnityAction<NetworkAction> listener)
    {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<NetworkAction> listener)
    {
        if (instance == null)
        {
            return;
        }
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, NetworkAction arg)
    {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(arg);
        }
    }

    public static void AddNetworkEvent(NetworkAction action)
    {
        lock (queueLock)
        {
            actionQueue.Add(action);
        }
    }
}
