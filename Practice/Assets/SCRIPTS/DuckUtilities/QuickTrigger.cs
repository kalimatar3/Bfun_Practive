using UnityEngine;
using UnityEngine.Events;

public class QuickTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent trigger;
    private void OnEnable()
    {
        trigger?.Invoke();
    }
}
