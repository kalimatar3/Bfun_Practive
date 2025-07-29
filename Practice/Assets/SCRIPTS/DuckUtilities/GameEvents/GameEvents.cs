using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DuckGame.Ultilities
{
    [CreateAssetMenu(menuName = "Game Event", fileName = "New Game Event")]
    public class GameEvents : ScriptableObject
    {
        HashSet<GameEventsListener> _listeners = new HashSet<GameEventsListener>();

        public void Invoke(MonoBehaviour script)
        {
            foreach (var globalEventListener in _listeners)
                globalEventListener.RaiseEvent(script);
        }

        public void Register(GameEventsListener gameEventsListener) => _listeners.Add(gameEventsListener);

        public void Deregister(GameEventsListener gameEventsListener) => _listeners.Remove(gameEventsListener);

    }
}



