using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;

    public UnityEvent<T> reponse;

    private void OnEnable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised += OnEventRaised;
    }
    private void DisEnable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised -= OnEventRaised;
    }

    private void OnEventRaised(T value)
    {
        reponse.Invoke(value);
    }
}
