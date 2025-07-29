using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Sirenix.OdinInspector;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cinemachine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace DuckGame.Ultilities
{
    public class GameManager : Singleton<GameManager>
    {
        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
        public void LoadtoHome()
        {
            SceneLoader.LoadScene((int)MyScene.Home,() => 
            {
            });
        }
    }
}
