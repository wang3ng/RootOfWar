/*
 * Advanced C# messenger by Ilya Suzdalnitski. V1.0
 * 
 * Based on Rod Hyde's "CSharpMessenger" and Magnus Wolffelt's "CSharpMessenger Extended".
 * (Modified by Dylan Bennett to add Unity 2017+ Scene Management support and switch to enums 
 * instead of strings for names of events.) 
 * 
 * Features:
 	* Prevents a MissingReferenceException because of a reference to a destroyed message handler.
 	* Option to log all messages
 	* Extensive error detection, preventing silent bugs
 * 
 * Usage examples:
 	1. Messenger.AddListener<GameObject>("prop collected", PropCollected);
 	   Messenger.Broadcast<GameObject>("prop collected", prop);
 	2. Messenger.AddListener<float>("speed changed", SpeedChanged);
 	   Messenger.Broadcast<float>("speed changed", 0.5f);
 * 
 * Messenger cleans up its evenTable automatically upon loading of a new level.
 * 
 * Don't forget that the messages that should survive the cleanup, should be marked with Messenger.MarkAsPermanent(string)
 * 
 */

//#define LOG_ALL_MESSAGES
//#define LOG_ADD_LISTENER
//#define LOG_BROADCAST_MESSAGE
//#define REQUIRE_LISTENER

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Events {
    checkTimeRemain,
    useCard,
    checkCard,
    move,
    attack,
    timeStart,
    equip,
    drawCard,
    turn,
    up,
    win,
    /**
    itemget,
    Dialogue,
    Dialogueoff,
    Usingitem,
    PickUpItem,
    EventHappened,
    EnterDoor,
    EnterPlace,
    EnterPlaceoff,
    unlock**/
}

static internal class Messenger {
    #region Internal variables

    //Disable the unused variable warning
#pragma warning disable 0414
    //Ensures that the MessengerHelper will be created automatically upon start of the game.
    static private MessengerHelper messengerHelper = (new GameObject("MessengerHelper")).AddComponent<MessengerHelper>();
#pragma warning restore 0414

    static public Dictionary<Events, Delegate> eventTable = new Dictionary<Events, Delegate>();

    //Message handlers that should never be removed, regardless of calling Cleanup
    static public List<Events> permanentMessages = new List<Events>();
    #endregion
    #region Helper methods
    //Marks a certain message as permanent.
    static public void MarkAsPermanent(Events eventType) {
#if LOG_ALL_MESSAGES
		Debug.Log("Messenger MarkAsPermanent \t\"" + eventType.ToString() + "\"");
#endif

        permanentMessages.Add(eventType);
    }


    static public void Cleanup() {
#if LOG_ALL_MESSAGES
		Debug.Log("MESSENGER Cleanup. Make sure that none of necessary listeners are removed.");
#endif

        List<Events> messagesToRemove = new List<Events>();

        foreach (KeyValuePair<Events, Delegate> pair in eventTable) {
            bool wasFound = false;

            foreach (Events message in permanentMessages) {
                if (pair.Key == message) {
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                messagesToRemove.Add(pair.Key);
        }

        foreach (Events message in messagesToRemove) {
            eventTable.Remove(message);
        }
    }

    static public void PrintEventTable() {
        Debug.Log("\t\t\t=== MESSENGER PrintEventTable ===");

        foreach (KeyValuePair<Events, Delegate> pair in eventTable) {
            Debug.Log("\t\t\t" + pair.Key.ToString() + "\t\t" + pair.Value);
        }

        Debug.Log("\n");
    }
    #endregion

    #region Message logging and exception throwing
    static public void OnListenerAdding(Events eventType, Delegate listenerBeingAdded) {
#if LOG_ALL_MESSAGES || LOG_ADD_LISTENER
		Debug.Log("MESSENGER OnListenerAdding \t\"" + eventType.ToString() + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
#endif

        if (!eventTable.ContainsKey(eventType)) {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType()) {
            throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType.ToString(), d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }

    static public void OnListenerRemoving(Events eventType, Delegate listenerBeingRemoved) {
#if LOG_ALL_MESSAGES
		Debug.Log("MESSENGER OnListenerRemoving \t\"" + eventType.ToString() + "\"\t{" + listenerBeingRemoved.Target + " -> " + listenerBeingRemoved.Method + "}");
#endif

        if (eventTable.ContainsKey(eventType)) {
            Delegate d = eventTable[eventType];

            if (d == null) {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType.ToString()));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType()) {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType.ToString(), d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
        }
        else {
            throw new ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType.ToString()));
        }
    }

    static public void OnListenerRemoved(Events eventType) {
        if (eventTable[eventType] == null) {
            eventTable.Remove(eventType);
        }
    }

    static public void OnBroadcasting(Events eventType) {
#if REQUIRE_LISTENER
        if (!eventTable.ContainsKey(eventType)) {
            throw new BroadcastException(string.Format("Broadcasting message \"{0}\" but no listener found. Try marking the message with Messenger.MarkAsPermanent.", eventType.ToString()));
        }
#endif
    }

    static public BroadcastException CreateBroadcastSignatureException(string eventType) {
        return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType.ToString()));
    }

    public class BroadcastException : Exception {
        public BroadcastException(string msg)
            : base(msg) {
        }
    }

    public class ListenerException : Exception {
        public ListenerException(string msg)
            : base(msg) {
        }
    }
    #endregion

    #region AddListener
    //No parameters
    static public void AddListener(Events eventType, Callback handler) {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Callback)eventTable[eventType] + handler;
    }

    //Single parameter
    static public void AddListener<T>(Events eventType, Callback<T> handler) {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Callback<T>)eventTable[eventType] + handler;
    }

    //Two parameters
    static public void AddListener<T, U>(Events eventType, Callback<T, U> handler) {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Callback<T, U>)eventTable[eventType] + handler;
    }

    //Three parameters
    static public void AddListener<T, U, V>(Events eventType, Callback<T, U, V> handler) {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] + handler;
    }
    #endregion

    #region RemoveListener
    //No parameters
    static public void RemoveListener(Events eventType, Callback handler) {
        OnListenerRemoving(eventType, handler);
        eventTable[eventType] = (Callback)eventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }

    //Single parameter
    static public void RemoveListener<T>(Events eventType, Callback<T> handler) {
        OnListenerRemoving(eventType, handler);
        eventTable[eventType] = (Callback<T>)eventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }

    //Two parameters
    static public void RemoveListener<T, U>(Events eventType, Callback<T, U> handler) {
        OnListenerRemoving(eventType, handler);
        eventTable[eventType] = (Callback<T, U>)eventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }

    //Three parameters
    static public void RemoveListener<T, U, V>(Events eventType, Callback<T, U, V> handler) {
        OnListenerRemoving(eventType, handler);
        eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
    #endregion

    #region Broadcast
    //No parameters
    static public void Broadcast(Events eventType) {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType.ToString() + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) {
            Callback callback = d as Callback;

            if (callback != null) {
                callback();
            }
            else {
                throw CreateBroadcastSignatureException(eventType.ToString());
            }
        }
    }

    //Single parameter
    static public void Broadcast<T>(Events eventType, T arg1) {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType.ToString() + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) {
            Callback<T> callback = d as Callback<T>;

            if (callback != null) {
                callback(arg1);
            }
            else {
                throw CreateBroadcastSignatureException(eventType.ToString());
            }
        }
    }

    //Two parameters
    static public void Broadcast<T, U>(Events eventType, T arg1, U arg2) {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType.ToString() + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) {
            Callback<T, U> callback = d as Callback<T, U>;

            if (callback != null) {
                callback(arg1, arg2);
            }
            else {
                throw CreateBroadcastSignatureException(eventType.ToString());
            }
        }
    }

    //Three parameters
    static public void Broadcast<T, U, V>(Events eventType, T arg1, U arg2, V arg3) {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType.ToString() + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) {
            Callback<T, U, V> callback = d as Callback<T, U, V>;

            if (callback != null) {
                callback(arg1, arg2, arg3);
            }
            else {
                throw CreateBroadcastSignatureException(eventType.ToString());
            }
        }
    }
    #endregion
}

//This manager will ensure that the messenger's eventTable will be cleaned up upon loading of a new level.
public sealed class MessengerHelper : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    //Clean up eventTable every time a new level loads.
    private void OnSceneChange(Scene prevScene, Scene curScene) {
        if (prevScene.name != null) Messenger.Cleanup();
    }
}

public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);