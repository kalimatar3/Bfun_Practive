using DuckGame.Ultilities;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundName soundName;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySound(soundName, AudioSetting.Default);
    }
}
