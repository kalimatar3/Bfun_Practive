using System;
using System.Collections.Generic;

public class Signal
{
 private event Action<SignalMessage> _event;

    private readonly List<Action<SignalMessage>> _listeners = new();

    public void AddListener(Action<SignalMessage> callback)
    {
        if (!_listeners.Contains(callback))
        {
            _event += callback;
            _listeners.Add(callback);
        }
    }

    public void RemoveListener(Action<SignalMessage> callback)
    {
        if (_listeners.Contains(callback))
        {
            _event -= callback;
            _listeners.Remove(callback);
        }
    }

    public void Send(SignalMessage message)
    {
        _event?.Invoke(message);
    }

    public void ClearAllListeners()
    {
        foreach (var callback in _listeners)
        {
            _event -= callback;
        }
        _listeners.Clear();
    }}
public struct SignalMessage
{
    public long TARGETID;
    public long ROOTID;
    public SignalType Type;
    public object Payload;
}
public enum SignalType { Refresh, Reset, FadeIn, FadeOut }
