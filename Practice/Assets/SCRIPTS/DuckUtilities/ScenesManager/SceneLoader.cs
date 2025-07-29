using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DuckGame.Ultilities
{
    public enum MyScene
    {
        Loading,
        Home,
        GamePlay,    
    }
public static class SceneLoader
    {
        public static void LoadScene(int buildIndex, Action onDoneLoad = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            LoadScene(SceneUtility.GetScenePathByBuildIndex(buildIndex), onDoneLoad, mode);
        }
        public static void LoadScene(string scenePath, Action onDoneLoad = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            switch (mode)
            {
                case LoadSceneMode.Single:
                    ScenesManager.Instance.StartCoroutine(LoadSceneInternal(scenePath, onDoneLoad));
                    break;
                case LoadSceneMode.Additive:
                    SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
        private static IEnumerator LoadSceneInternal(string scenePath, Action onDoneLoad = null)
        {
            var buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
            if (Debug.isDebugBuild)
                Debug.Log($"Loading Scene {scenePath} at Build Index {buildIndex}");

            // get current scene and set a loading scene as active
            var currentScene = SceneManager.GetActiveScene();
            var loadingScene = SceneManager.CreateScene("Loading_Background");
            SceneManager.SetActiveScene(loadingScene);

            // unload last scene
            var unload = SceneManager.UnloadSceneAsync(currentScene, UnloadSceneOptions.None);
            while (!unload.isDone)
            {
                Debug.Log("Isunload");
                yield return null;
            }

            // clean up
            var clean = Resources.UnloadUnusedAssets();
            while (!clean.isDone)
            {
                Debug.Log("IsCLear");
                yield return null;
            }

            // load new scene
            var load = new AsyncOperation();
#if UNITY_EDITOR
            if (buildIndex == -1)
            {
                load = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath,
                    new LoadSceneParameters(LoadSceneMode.Single));
            }
            else
            {
                load = SceneManager.LoadSceneAsync(buildIndex);
            }
#else
            load = SceneManager.LoadSceneAsync(scenePath);
#endif
            while (!load.isDone)
            {
                Debug.Log("Isload");
                yield return null;
            }

            onDoneLoad?.Invoke();
        }
    }
}