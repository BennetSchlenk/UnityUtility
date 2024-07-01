using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase
{
    public DateTime Timestamp { get; private set; }
    public object Sender { get; private set; }
    
    protected EventBase(object sender)
    {
        Timestamp = DateTime.UtcNow;
        Sender = sender;
    }

    public override string ToString()
    {
        return $"Event Timestamp: {Timestamp}, Sender: {Sender}";
    }
}
