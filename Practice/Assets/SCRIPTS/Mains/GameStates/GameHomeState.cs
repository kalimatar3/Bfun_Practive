using DuckGame.Ultilities;
using UnityEngine;

public class GameHomeState : GameState
{
    public override void EnterState()
    {
        base.EnterState();
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        //SPawn Vehicle;
        GUIManager.Instance.CloseBackGround();
        //PopupManager.Popup.ShowPopup(PopupHome.Main);
    }
}