using System.Collections;
using System.Collections.Generic;
using Clouds.Ultilities;
using UnityEngine;

public class Player : Singleton<Player>
{
    public Signal PlayerSignal,Signal2,Signal3;
    public SignalMessage signalMessage;
    public SignalMessage Opa1Message;
    public SignalMessage Opa2Message;
    public Vector3 MoveDirection;
    float Timer = 0;
    protected void Start()
    {
        MoveDirection = Vector3.forward;
        PlayerSignal = new Signal();
        Signal2 = new Signal();
        Signal3 = new Signal();
    }
    public void FixedUpdate()
    {
        this.transform.Translate(MoveDirection.normalized);
        Timer += Time.deltaTime;
        if (Timer > 5f)
        {
            Timer = 0;
            MoveDirection = new Vector3(Random.Range(.5f,1f), 0, Random.Range(.5f,1f));
            signalMessage.Value = MoveDirection.ToString();
            Opa1Message.Value = (float)MoveDirection.x;
            Opa2Message.Value = (float)MoveDirection.z;
            PlayerSignal.Send(signalMessage);
            Signal2.Send(Opa1Message);
            Signal3.Send(Opa2Message);
        }
    }
}
