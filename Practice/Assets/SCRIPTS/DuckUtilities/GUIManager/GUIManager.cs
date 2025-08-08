// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Sirenix.OdinInspector;
// using TMPro;
// using DG.Tweening;
// using UnityEngine.AddressableAssets;
// using System;

// namespace DuckGame.Ultilities
// {
//     public enum BUTTONCONTROLTYPE { Gas, Brake, HandBrake, Left, Right };
//     public enum GUITEMPLATE{Loading,Home, Game, MultiScene}
//     public enum LEVELTHEME {GasStation, Snow, Port, Village, Desert}
//     public enum PANEL {DailyLogin, Main, Inspect, Store, RimCustomize, PaintCustomize, Level, Setting, SelectMode, VehicleInfo, LevelChapter, LevelProgession, VisualUpgrade, VehicleUpgade, VehicleUnlock, SuspensionCustomize}

//     public class GUIManager : Singleton<GUIManager>
//     {
//         [ReadOnly] public GUITEMPLATE currentGUI;
//         [ReadOnly] public PANEL currentPanel;
//         [ReadOnly] public PANEL lastUsedPanel;

//         [BoxGroup("System")] public AssetReferenceGameObject BackGroundUI;
//         [BoxGroup("System")] public AssetReferenceGameObject HomeCanvas;
//         [BoxGroup("System")] public AssetReferenceGameObject GameCanvas;
//         [BoxGroup("System")] public AssetReferenceGameObject MultiSceneCanvas;
//         [BoxGroup("System")] [ReadOnly] public PopupGUI currentPopupGUI;
//         [BoxGroup("System")] [ReadOnly] public PopupGUI popupGUIMultiScene;

//         [BoxGroup("System")]
//         public GameObject EventSystem;
//         [BoxGroup("System")]
//         public Camera cameraUI;

//         [BoxGroup("Effect")]
//         public GameObject moneyParticleEffect;

//         [BoxGroup("Home")][ReadOnly] public CanvasHome canvasHome;
//         [BoxGroup("Home")][ReadOnly] public CanvasGame canvasGame;
//         [BoxGroup("Home")][ReadOnly] public CanvasMultiScene canvasMultiScene;
//         [ShowInInspector] public Dictionary<string, GameObject> allAppliedDecalGameObject = new Dictionary<string, GameObject>();

//         GameObject homeCanvasObj;
//         GameObject gameCanvasObj;
//         GameObject multiSceneCanvasObj;
//         GameObject background;
//         public bool CanInteractiveWithButton
//         {
//             get
//             {
//                 return DOTween.TotalTweensById(1, true) == 0;
//             }
//         }

//         public void DelayPress()
//         {
//             StartCoroutine(IDelayPress());
//         }

//         IEnumerator IDelayPress()
//         {
//             EventSystem.SetActive(false);
//             yield return new WaitForSecondsRealtime(0.5f);
//             EventSystem.SetActive(true);
//         }

//         public void SwitchCanvas(GUITEMPLATE canvas, Action onDone = null)
//         {
//             StartCoroutine(ISwitchCanvas(canvas, onDone));
//         }

//         public void SwitchPanel(PANEL panel)
//         {
//             currentPanel = panel;
//         }

//     public IEnumerator ShowBackground(Action onDoneLoad = null , bool withClose = false, float timeWait = 0, Action onCloseLoad = null)
//     {
//         yield return IShowBackgound(onDoneLoad, withClose, timeWait, onCloseLoad);
//     }

//     IEnumerator IShowBackgound(Action onDoneLoad = null, bool withClose = false, float timeWait = 0, Action onCloseLoad = null)
//     {
//         DuckHelper.LogEvent("x_screen_loading");
//         allAppliedDecalGameObject.Clear();
//         Application.backgroundLoadingPriority = ThreadPriority.High;
//         var backgroundAsset = BackGroundUI.InstantiateAsync(transform);

//         backgroundAsset.Completed += (asyncOperation) =>
//         {
//             Application.backgroundLoadingPriority = ThreadPriority.Normal;
//             background= asyncOperation.Result;
//             background.GetComponent<Canvas>().worldCamera = Camera.main;
//             background.GetComponent<CanvasGroup>().alpha = 0;
//         };

//         yield return backgroundAsset;
//         background.GetComponent<CanvasGroup>().DOFade(1, 0.75f).SetUpdate(true).OnComplete(() =>
//         {
//             onDoneLoad?.Invoke();
//             SoundManager.Instance.MuteSound(true, true);
//             });

//         if(withClose) 
//         {
//             yield return new WaitForSeconds(timeWait);
//             CloseBackGround();
//             yield return new WaitForSeconds(0.75f);
//             onCloseLoad?.Invoke();
//         }

//     }
//     public void CloseBackGround()
//     {
//         background.GetComponent<CanvasGroup>().DOFade(0, 0.75f).SetUpdate(true).OnComplete(() =>
//         {
//             BackGroundUI?.ReleaseInstance(background);
//             SoundManager.Instance.MuteSound(false, true);
//         });
//     }

//     IEnumerator ISwitchCanvas(GUITEMPLATE canvas, Action onDone = null)
//         {
//             canvasHome = null;
//             canvasGame = null;
//             currentGUI = canvas;
//             switch (canvas)
//             {
//                 case GUITEMPLATE.Home:
//                     if(gameCanvasObj) GameCanvas?.ReleaseInstance(gameCanvasObj);
//                     Application.backgroundLoadingPriority = ThreadPriority.High;
//                     var homeCanvasAsset = HomeCanvas.InstantiateAsync(transform);
//                     homeCanvasAsset.Completed += (asyncOperation) =>
//                     {
//                         Application.backgroundLoadingPriority = ThreadPriority.Normal;
//                         homeCanvasObj = asyncOperation.Result;
//                         homeCanvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
//                     };
//                     yield return homeCanvasAsset;
//                     canvasHome = homeCanvasObj.GetComponent<CanvasHome>();
//                     canvasHome.canvas.worldCamera = cameraUI;
//                     currentPopupGUI = homeCanvasObj.GetComponent<PopupGUI>();
//                     onDone?.Invoke();

//                     break;
//                 case GUITEMPLATE.Game:
//                     if (homeCanvasObj) HomeCanvas?.ReleaseInstance(homeCanvasObj);
//                     if (gameCanvasObj) GameCanvas?.ReleaseInstance(gameCanvasObj);

//                     Application.backgroundLoadingPriority = ThreadPriority.High;
//                     var gameCanvasAsset = GameCanvas.InstantiateAsync(transform);

//                     gameCanvasAsset.Completed += (asyncOperation) =>
//                     {
//                         Application.backgroundLoadingPriority = ThreadPriority.Normal;
//                         gameCanvasObj = asyncOperation.Result;
//                         gameCanvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
//                     };

//                     yield return gameCanvasAsset;
//                     canvasGame = gameCanvasObj.GetComponent<CanvasGame>();
//                     canvasGame.canvas.worldCamera = cameraUI;
//                     currentPopupGUI = gameCanvasObj.GetComponent<PopupGUI>();
//                     onDone?.Invoke();

//                     break;
//             }
//         }

//         public void LoadMultiSceneCanvas()
//         {
//             StartCoroutine(ILoadMultiSceneCanvas());
//         }

//         IEnumerator ILoadMultiSceneCanvas()
//         {   
//             if (multiSceneCanvasObj) MultiSceneCanvas?.ReleaseInstance(multiSceneCanvasObj);
//             Application.backgroundLoadingPriority = ThreadPriority.High;
//             var multiSceneCanvasAsset = MultiSceneCanvas.InstantiateAsync(transform);
//             multiSceneCanvasAsset.Completed += (asyncOperation) =>
//             {
//                 Application.backgroundLoadingPriority = ThreadPriority.Normal;
//                 multiSceneCanvasObj = asyncOperation.Result;
//                 multiSceneCanvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
//             };

//             yield return multiSceneCanvasAsset;
//             canvasMultiScene = multiSceneCanvasObj.GetComponent<CanvasMultiScene>();
//             canvasMultiScene.canvas.worldCamera = cameraUI;
//             popupGUIMultiScene = multiSceneCanvasObj.GetComponent<PopupGUI>();
//         }

//         public void SetLastUsePanel()
//         {
//             lastUsedPanel = currentPanel;
//         }

//         //------------------------------------------------

//         public void SpawnMoneyParticleEffect(Transform spawnHolder ,Transform target)
//         {
//             GameObject particle = Instantiate(moneyParticleEffect, spawnHolder);
//             //AssetKits.ParticleImage.ParticleImage particleImage = particle.GetComponent<AssetKits.ParticleImage.ParticleImage>();
//         }
//     }
// }
