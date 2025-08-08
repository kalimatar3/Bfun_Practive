using System;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
namespace Clouds.Ultilities
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        public const string LOADINGSCENENAME = "0_Splash_Landscape";
        public const string HOMESCENENAME = "Home";
        public string GAMEPLAYSCENE = "Cliff";
        public string FINISHSCENE = "FinishScene_Cliff";
        public string TESTSCENE = "TestScene";
        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}