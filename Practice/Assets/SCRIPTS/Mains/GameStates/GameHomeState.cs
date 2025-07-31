using DuckGame.Ultilities;
using UnityEngine;

public class GameHomeState : GameState
{
    public override void EnterState()
    {
        base.EnterState();
        GUIManager.Instance.SwitchCanvas(GUITEMPLATE.Home, () =>
        {
            PopupManager.Popup.ShowPopup(PopupHome.Main);
        });
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}