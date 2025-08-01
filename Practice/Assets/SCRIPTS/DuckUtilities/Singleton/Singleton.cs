using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame.Ultilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if(_instance == null)
                    {
                        var singletonObj = new GameObject();
                        singletonObj.name = typeof(T).ToString();
                        _instance = singletonObj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public virtual void Awake()
        {
            if(_instance != null)
            {
                //Destroy(gameObject);
                return;
            }

            _instance = GetComponent<T>();

            if (_instance == null)
                return;
        }
    }
}
