using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour, IEventManager
{
    private Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();


    public void AddListener<T>(Action<T> listener) where T : EventBase
    {
        var eventType = typeof(T);
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Delegate>();
        }

        eventListeners[eventType].Add(listener);
    }

    public void RemoveListener<T>(Action<T> listener) where T : EventBase
    {
        var eventType = typeof(T);
        if (!eventListeners.ContainsKey(eventType)) return;
        
        eventListeners[eventType].Remove(listener);
        if (eventListeners[eventType].Count == 0)
        {
            eventListeners.Remove(eventType);
        }
    }

    public void Trigger<T>(T eventArgs) where T : EventBase
    {
        var eventType = typeof(T);
        if (!eventListeners.ContainsKey(eventType)) return;
        
        foreach (var listener in eventListeners[eventType])
        {
            ((Action<T>)listener)?.Invoke(eventArgs);
        }
    }
}