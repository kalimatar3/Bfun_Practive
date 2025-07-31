using System;

public class Signal
{
    public Action<SignalMessage> Event ;
    public void Send(SignalMessage signalMessage)
    {
        Event?.Invoke(signalMessage);
    }

}
public struct SignalMessage
{
    public long TARGETID;
    public long ROOTID;
    
}
