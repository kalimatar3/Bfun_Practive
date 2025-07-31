using DuckGame.Ultilities;
using UnityEngine;

public class GameLoadingState : GameState
{
    public override void EnterState()
    {
        base.EnterState();
        Application.backgroundLoadingPriority = ThreadPriority.High;
    }
    public override void ExitState()
    {
        base.ExitState();
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
    }
}