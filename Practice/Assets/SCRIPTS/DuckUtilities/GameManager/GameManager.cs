using UnityEngine;
namespace Clouds.Ultilities
{
    public class GameManager : Singleton<GameManager>
    {
        [HideInInspector] public SceneData CurSceneData;
        public GameStateController StateController;
        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
        protected void Start()
        {
            this.LoadtoHome();
        }
        public void LoadtoHome()
        {
            StateController.Statemachine.ChangeState((int)GameStateEnum.Loading);
            SceneLoader.LoadScene("Home", () =>
            {
                CurSceneData = HomeSceneData.Instance;
                CurSceneData.Init();
                StateController.Statemachine.ChangeState((int)GameStateEnum.Home);
            });
        }
        public void LoadtoGame()
        {
            
        }
    }
}
