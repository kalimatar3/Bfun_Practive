using DuckGame.Ultilities;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLoadingState : GameState
{    
    public override void EnterState()
    {
        base.EnterState();
        Application.backgroundLoadingPriority = ThreadPriority.High;
        //DataManager.InitData();
        SceneLoader.LoadScene(ScenesManager.HOMESCENENAME,()=> {
            // GUIManager.Instance.CloseBackGround();
            // Statemachine.ChangeState((int)GameStateEnum.Home);
        });
    }
    public override void ExitState()
    {
        base.ExitState();
        GUIManager.Instance.ShowBackground();
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
    }
}