using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event<T> where T : Event<T>
{
    private bool _hasFired;
    public delegate void EventListener(T info);
    private static event EventListener Listeners;

    public static void RegisterListener(EventListener listener)
    {
        Listeners += listener;
    }

    public static void UnregisterListener(EventListener listener)
    {
        Listeners -= listener;
    }

    public void FireEvent()
    {
        if (_hasFired)
        {
            throw new Exception("This event has already fired, to prevent infinite loops you can't refire an event");
        }
        _hasFired = true;
        if (Listeners != null)
        {
            Listeners(this as T);
        }
    }
}
