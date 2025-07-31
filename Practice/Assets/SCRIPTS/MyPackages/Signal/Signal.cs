using System;
using UnityEngine;
using UnityEngine.UI;

public class SignalContainer
{
    public Action<Signal> Event;
    public void Send(Signal signalMessage)
    {
        Event?.Invoke(signalMessage);
    }
}
