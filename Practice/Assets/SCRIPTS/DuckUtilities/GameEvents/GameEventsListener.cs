using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;

namespace DuckGame.Ultilities
{
    public class GameEventsListener : MonoBehaviour
    {
        [SerializeField] GameEvents _gameEvent;
        [SerializeField] UnityEvent _unityEvent;
    #if UNITY_EDITOR
        MonoScript[] AllScriptsInProject => GetScriptAssetsOfType<MonoBehaviour>();
        [ShowInInspector]
        [ReadOnly]
        List<MonoScript> WhereIsThisEventBeingCalled = new List<MonoScript>();
    #endif

        private void Awake() => _gameEvent.Register(this);

        private void OnDestroy() => _gameEvent.Deregister(this);

        public void RaiseEvent(MonoBehaviour script)
        {
    #if UNITY_EDITOR
            WhereIsThisEventBeingCalled.Add(MonoScript.FromMonoBehaviour(script));
            SaveEventList();
    #endif
            _unityEvent.Invoke();
        }

    #if UNITY_EDITOR
        private void SaveEventList()
        {
            PlayerPrefs.SetInt("ListCount", WhereIsThisEventBeingCalled.Count);
            if (WhereIsThisEventBeingCalled.Count > 1)
            {
                for (int i = 0; i < WhereIsThisEventBeingCalled.Count; i++)
                {
                    PlayerPrefs.SetString("Monobehavior" + i + _gameEvent, WhereIsThisEventBeingCalled[i].name);
                }
            }
            else
            {
                PlayerPrefs.SetString("Monobehavior" + _gameEvent, WhereIsThisEventBeingCalled[0].name);
            }
        }

        [OnInspectorInit]
        private void LoadEventList()
        {
            if (PlayerPrefs.HasKey("ListCount"))
            {
                if (PlayerPrefs.GetInt("ListCount") > 1)
                {
                    for (int i = 0; i < PlayerPrefs.GetInt("ListCount"); i++)
                    {
                        if (PlayerPrefs.HasKey("Monobehavior" + i + _gameEvent))
                        {
                            if (!CheckItemHasAlreadyInList(PlayerPrefs.GetString("Monobehavior" + i + _gameEvent)))
                                WhereIsThisEventBeingCalled.Add(FindScirpt(PlayerPrefs.GetString("Monobehavior" + i + _gameEvent)));
                        }
                    }
                }

                else
                {
                    if (PlayerPrefs.HasKey("Monobehavior" + _gameEvent))
                    {
                        if (!CheckItemHasAlreadyInList(PlayerPrefs.GetString("Monobehavior" + _gameEvent)))
                        {
                            WhereIsThisEventBeingCalled.Add(FindScirpt(PlayerPrefs.GetString("Monobehavior" + _gameEvent)));
                        }
                    }
                }
            }
            
            else 
                WhereIsThisEventBeingCalled.Clear();
        }

        private MonoScript[] GetScriptAssetsOfType<T>()
        {
            MonoScript[] scripts = (MonoScript[])Resources.FindObjectsOfTypeAll(typeof(MonoScript));

            List<MonoScript> result = new List<MonoScript>();

            foreach (MonoScript m in scripts)
            {
                if (m.GetClass() != null && m.GetClass().IsSubclassOf(typeof(T)))
                {
                    result.Add(m);
                }
            }
            return result.ToArray();
        }


        private MonoScript FindScirpt(string name)
        {
            MonoScript target = null;
            foreach (MonoScript script in AllScriptsInProject)
            {
                if (script.name == name)
                    target = script;
            }

            return target;
        }

        private bool CheckItemHasAlreadyInList(string name)
        {
            bool check = false;
            if (WhereIsThisEventBeingCalled != null)
            {
                foreach (MonoScript script in WhereIsThisEventBeingCalled)
                {
                    if (script.name == name)
                        check = true;
                }
            }
            return check;
        }
    #endif
    }
}
