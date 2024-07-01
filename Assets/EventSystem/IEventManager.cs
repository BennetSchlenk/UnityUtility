using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventManager
{
    void AddListener<T>(Action<T> listener) where T : EventBase;
    void RemoveListener<T>(Action<T> listener) where T : EventBase;
    void Trigger<T>(T eventArgs) where T : EventBase;
}
