using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DuckGame.Ultilities
{
    public enum TYPEOFF {Call, Destroy, Disable}

    [System.Serializable]
    public struct EventDelay
    {
        public TYPEOFF type;
        public float timeDelay;
        public UnityEvent onDelay;
        public bool disableAtEnd;
    }
    public class EventDelayCaller: MonoBehaviour
    {
        [SerializeField] EventDelay[] eventDelays;

        private void Start() {
            
        }
        private void OnEnable() {
            foreach(EventDelay ed in eventDelays)
            {
                StartCoroutine(IDelay(ed));
            }
        }

        IEnumerator IDelay(EventDelay ed)
        {
            yield return new WaitForSeconds(ed.timeDelay);
                ed.onDelay?.Invoke();
                switch(ed.type)
                {
                    case TYPEOFF.Destroy:
                        Destroy(gameObject);
                        break;
                    case TYPEOFF.Disable:
                        gameObject.SetActive(false);
                        break;
                }
                if(ed.disableAtEnd) this.enabled = false;
        }
    }
}
