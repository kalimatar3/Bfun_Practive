using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
using System.Linq;
using System.IO;
using UnityEditor;
using DG.Tweening;
using Unity.VisualScripting;

namespace Clouds.Ultilities
{
    public enum AudioType{ Sound, Music}

    [System.Serializable]
    public struct AudioSetting
    {
        public bool fade;
        public bool loop;
        public int priority;
        public float pitch;
        public float volume;
        public AudioType audioType;
        public bool noneSeamlessly;

        public static AudioSetting Default
        {
            get
            {
                AudioSetting defaultOptions = new AudioSetting();
                defaultOptions.fade = false;
                defaultOptions.loop = false;
                defaultOptions.priority = 128;
                defaultOptions.pitch = 1;
                defaultOptions.volume = 1;
                return defaultOptions;
            }
        }

        public static AudioSetting DefaultFade
        {
            get
            {
                AudioSetting defaultOptions = new AudioSetting();
                defaultOptions.fade = true;
                defaultOptions.loop = false;
                defaultOptions.priority = 128;
                defaultOptions.pitch = 1;
                defaultOptions.volume = 1;
                return defaultOptions;
            }
        }

        public static AudioSetting Loop
        {
            get
            {
                AudioSetting loopOptions = new AudioSetting();
                loopOptions.fade = false;
                loopOptions.loop = true;
                loopOptions.priority = 128;
                loopOptions.pitch = 1;
                loopOptions.volume = 1;
                return loopOptions;
            }
        }

        public static AudioSetting Music
        {
            get
            {
                AudioSetting musicOptions = new AudioSetting();
                musicOptions.fade = true;
                musicOptions.audioType = AudioType.Music;
                musicOptions.loop = true;
                musicOptions.priority = 128;
                musicOptions.pitch = 1;
                musicOptions.volume = 1;
                return musicOptions;
            }
        }

        public static AudioSetting MusicNoLoop
        {
            get
            {
                AudioSetting musicOptions = new AudioSetting();
                musicOptions.fade = true;
                musicOptions.audioType = AudioType.Music;
                musicOptions.loop = false;
                musicOptions.priority = 128;
                musicOptions.pitch = 1;
                musicOptions.volume = 1;
                musicOptions.noneSeamlessly = true;
                return musicOptions;
            }
        }
    }

    [System.Serializable]
    public struct SoundData{
        [TitleGroup("Sound", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [GUIColor(0,1,0)] public string  soundName;
        public AudioClip clip;
    }

    [HideMonoScript]
    public class SoundManager : Singleton<SoundManager>
    {
        public float SoundVolume{
            get 
            {
                return Volume;
            }
            set
            {
                Volume = value;
                AudioListener.volume = value;
            }
        }

        [HideLabel]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        public Sprite Icon;
        [TitleGroup("SOUND MANAGER", "@DuckGame", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [Header("Game Volume")]
        [ProgressBar(0, 1)]
        float Volume;

        [Header("Mixer")]
        [SerializeField] public AudioMixer GameAudioMixer;
        [SerializeField] public AudioMixerGroup SFX;
        [SerializeField] private AudioMixerGroup Music;

        [Header("Pooling")]
        [SerializeField] private GameObject audioPrefab;
        [SerializeField] AudioClip[] BGMs;

        [SerializeField] AudioClip[] BGMInGame;

        public List<SoundData> listSound = new List<SoundData>();
        [ReadOnly] public bool isFading;
        Dictionary<SoundName, SoundData> dicSound = new Dictionary<SoundName, SoundData>();

        int currentBGM, currentIngameBGM;
        Coroutine waitBGMCour, waitBGMIngameCour;
        bool isMuteCurrentMusic;

        [Button(ButtonSizes.Large), GUIColor(0,1,0)]
        #if UNITY_EDITOR
        private void SAVESOUND()
        {
            string enumName = "SoundName";
            string filePathAndName = "Assets/DuckUtilities/SoundManager/" + enumName + ".cs";

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("namespace DuckGame.Ultilities");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine("public enum " + enumName);
                streamWriter.WriteLine("{");
                for (int i = 0; i < listSound.Count; i++)
                {
                    streamWriter.WriteLine("\t" + listSound[i].soundName + ",");
                }
                streamWriter.WriteLine("}");
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
    #endif

        public override void Awake() {
            base.Awake();

            foreach(SoundData data in listSound)
            {
                SoundName soundName;
                System.Enum.TryParse(data.soundName, out soundName);
                dicSound.Add(soundName, data);
            }

            GameAudioMixer.SetFloat("MusicVolume", GameDatas.MusicValue);
            GameAudioMixer.SetFloat("SFXVolume", GameDatas.SoundValue);
        }

        public void PlayBGM(float volume = 1)
        {
            if (BGMs.Length == 0) return;
            currentBGM = Random.Range(0, BGMs.Length);
            PlaySound(BGMs[currentBGM], AudioSetting.MusicNoLoop, isMuteCurrentMusic ? 0 : volume);
            waitBGMCour = StartCoroutine(IWatingBGM(BGMs[currentBGM].length, BGMs[currentBGM], volume));
        }

        public void MuteCurrentBGM(bool mute)
        {
            isMuteCurrentMusic = mute;
            AudioSource audioResult = null;
            foreach (Transform tp in transform)
            {
                if (tp.name == BGMs[currentBGM].name)
                {
                    audioResult = tp.GetComponent<AudioSource>();
                }
            }

            if (audioResult != null)
            {
                DOVirtual.Float(audioResult.volume, mute ? 0 : 1, 0.75f, (value) =>
                {
                    audioResult.volume = value;
                });
            }
        }

        IEnumerator IWatingBGM(float time, AudioClip clip, float volume)
        {
            yield return new WaitForSeconds(time - 1f);
            StopSoundFade(clip);
            yield return new WaitForSeconds(2.5f);
            PlayBGM(volume);
        }

        public void StopBGM()
        {
            if (BGMs.Length == 0) return;
            StopSoundFade(BGMs[currentBGM]);
            if (waitBGMCour != null) StopCoroutine(waitBGMCour);
        }

        public void PlayBGMInGame(float volume = 1)
        {
            currentIngameBGM = Random.Range(0, BGMInGame.Length);
            PlaySound(BGMInGame[currentIngameBGM], AudioSetting.Music, volume);
        }
        IEnumerator IWatingBGMInGame(float time, AudioClip clip, float volume)
        {
            yield return new WaitForSeconds(time - 1f);
            StopSoundFade(clip);
            yield return new WaitForSeconds(2.5f);
            if(waitBGMCour != null)
                PlayBGMInGame(volume);
        }

        public void StopBGMInGame()
        {
            StopSoundFade(BGMInGame[currentIngameBGM]);
            //if (waitBGMIngameCour != null) StopCoroutine(waitBGMIngameCour);
        }

        public void PlayUIClickSound()
        {
            //PlaySound(SoundName.ButtonClick, AudioSetting.Default);
        }

        public void PlaySound(SoundName sound, AudioSetting setting, float volume = 1) {
            if(sound == SoundName.None) return;

            GameObject poolAudio = Instantiate(audioPrefab, transform);
            poolAudio.name = dicSound[sound].clip.name;
            poolAudio.GetComponent<AudioSource>().clip = dicSound[sound].clip;
            poolAudio.GetComponent<AudioSource>().loop = setting.loop;
            poolAudio.GetComponent<AudioSource>().priority = setting.priority;
            poolAudio.GetComponent<AudioSource>().pitch = setting.pitch;
            poolAudio.GetComponent<AudioSource>().volume = volume;
            poolAudio.GetComponent<AudioSource>().spatialBlend = 0;
            poolAudio.GetComponent<AudioSource>().minDistance = 1;
            
    
            switch(setting.audioType) {
                case AudioType.Sound :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                    break;
                case AudioType.Music :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                    break;
            }

            poolAudio.GetComponent<AudioSource>().Play();
            
            if(!setting.loop && !setting.noneSeamlessly) 
                StartCoroutine(ReleaseSound(dicSound[sound].clip, poolAudio));
        }

        public void PlaySoundRandomPitch3D(SoundName sound, AudioSetting setting, Vector3 pos, float volume = 1, float formPitchRandomMultiplier = 0, float toPitchRandomMultiplier = 1, float minDistance = 10, float maxDistance = 500) {
            if(sound == SoundName.None) return;
            
            if(CheckAlotSoundInRange3D(dicSound[sound].clip, pos))
            {
                GameObject poolAudio = Instantiate(audioPrefab, transform);
                poolAudio.transform.position = pos;
                poolAudio.name = dicSound[sound].clip.name;
                poolAudio.GetComponent<AudioSource>().clip = dicSound[sound].clip;
                poolAudio.GetComponent<AudioSource>().loop = setting.loop;
                poolAudio.GetComponent<AudioSource>().priority = setting.priority;

                poolAudio.GetComponent<AudioSource>().pitch *= Random.Range(formPitchRandomMultiplier, toPitchRandomMultiplier);

                poolAudio.GetComponent<AudioSource>().volume = volume;
                poolAudio.GetComponent<AudioSource>().spatialBlend = 1;
                poolAudio.GetComponent<AudioSource>().minDistance = minDistance;
                poolAudio.GetComponent<AudioSource>().maxDistance = maxDistance;

                switch(setting.audioType) {
                    case AudioType.Sound :
                        poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                        break;
                    case AudioType.Music :
                        poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                        break;
                }

                poolAudio.GetComponent<AudioSource>().Play();
                
                if(!setting.loop && !setting.noneSeamlessly) 
                    StartCoroutine(ReleaseSound(dicSound[sound].clip, poolAudio));
            }
        }

        public void PlaySoundRandomPitch3D(AudioClip sound, AudioSetting setting, Vector3 pos, float volume = 1, float formPitchRandomMultiplier = 0, float toPitchRandomMultiplier = 1, float minDistance = 10, float maxDistance = 500) {
            if(CheckAlotSoundInRange3D(sound, pos))
            {
                GameObject poolAudio = Instantiate(audioPrefab, transform);
                poolAudio.transform.position = pos;
                poolAudio.name = sound.name;
                poolAudio.GetComponent<AudioSource>().clip = sound;
                poolAudio.GetComponent<AudioSource>().loop = setting.loop;
                poolAudio.GetComponent<AudioSource>().priority = setting.priority;

                poolAudio.GetComponent<AudioSource>().pitch *= Random.Range(formPitchRandomMultiplier, toPitchRandomMultiplier);

                poolAudio.GetComponent<AudioSource>().volume = volume;
                poolAudio.GetComponent<AudioSource>().spatialBlend = 1;
                poolAudio.GetComponent<AudioSource>().minDistance = minDistance;
                poolAudio.GetComponent<AudioSource>().maxDistance = maxDistance;

                switch(setting.audioType) {
                    case AudioType.Sound :
                        poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                        break;
                    case AudioType.Music :
                        poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                        break;
                }

                poolAudio.GetComponent<AudioSource>().Play();
                
                if(!setting.loop && !setting.noneSeamlessly) 
                    StartCoroutine(ReleaseSound(sound, poolAudio));
            }
        }
        

        bool CheckAlotSoundInRange3D(AudioClip clip, Vector3 playAudioPos)
        {
            bool canPlay = true;
            AudioSource[] audio = transform.GetComponents<AudioSource>().Where(t => t.clip == clip).ToArray();
            foreach(AudioSource au in audio)
            {
                if(Vector3.Distance(au.transform.position, playAudioPos) < 5)
                    canPlay = false;
            }
            return canPlay;
        }

        public void PlaySound(AudioClip clip, AudioSetting setting, float volume = 1) {
            GameObject poolAudio = Instantiate(audioPrefab, transform);
            poolAudio.name = clip.name;
            poolAudio.GetComponent<AudioSource>().clip = clip;
            poolAudio.GetComponent<AudioSource>().loop = setting.loop;
            poolAudio.GetComponent<AudioSource>().priority = setting.priority;
            poolAudio.GetComponent<AudioSource>().pitch = setting.pitch;
            poolAudio.GetComponent<AudioSource>().volume = setting.fade ? 0 : volume;
            poolAudio.GetComponent<AudioSource>().spatialBlend = 0;
            poolAudio.GetComponent<AudioSource>().minDistance = 1;
            
    
            switch(setting.audioType) {
                case AudioType.Sound :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                    break;
                case AudioType.Music :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                    break;
            }

            poolAudio.GetComponent<AudioSource>().Play();

            if(setting.fade)
            {
                FadeIn(poolAudio.GetComponent<AudioSource>(), volume);
            }
            
            if(!setting.loop && !setting.noneSeamlessly)
                StartCoroutine(ReleaseSound(clip, poolAudio));
        }
        

        public void PlaySound3D(SoundName sound, AudioSetting setting, Vector3 pos, float volume = 1, float minDistance = 10, float maxDistance = 500, bool checkAlotSounb = true) {
            if(sound == SoundName.None) return;
            if(checkAlotSounb)
            {
                if(!CheckAlotSoundInRange3D(dicSound[sound].clip, pos)) return;
            }

            GameObject poolAudio = Instantiate(audioPrefab, transform);
            poolAudio.transform.position = pos;
            poolAudio.name = dicSound[sound].clip.name;
            poolAudio.GetComponent<AudioSource>().clip = dicSound[sound].clip;
            poolAudio.GetComponent<AudioSource>().loop = setting.loop;
            poolAudio.GetComponent<AudioSource>().priority = setting.priority;
            poolAudio.GetComponent<AudioSource>().pitch = setting.pitch;
            poolAudio.GetComponent<AudioSource>().volume = setting.fade ? 0 : volume;
            poolAudio.GetComponent<AudioSource>().spatialBlend = 1;
            poolAudio.GetComponent<AudioSource>().minDistance = minDistance;
            poolAudio.GetComponent<AudioSource>().maxDistance = maxDistance;

            switch(setting.audioType) {
                case AudioType.Sound :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                    break;
                case AudioType.Music :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                    break;
            }

            poolAudio.GetComponent<AudioSource>().Play();

            if(setting.fade)
            {
                FadeIn(poolAudio.GetComponent<AudioSource>(), volume);
            }
            
            if(!setting.loop && !setting.noneSeamlessly) 
                StartCoroutine(ReleaseSound(dicSound[sound].clip, poolAudio));
        }

        public void PlaySound3D(AudioClip clip, AudioSetting setting, Vector3 pos, float volume = 1, float minDistance = 10, float maxDistance = 500, bool checkAlotSounb = true) {

            if(checkAlotSounb)
            {
                if(!CheckAlotSoundInRange3D(clip, pos)) return;
            }

            GameObject poolAudio = Instantiate(audioPrefab, transform);
            poolAudio.transform.position = pos;
            poolAudio.name = clip.name;
            poolAudio.GetComponent<AudioSource>().clip = clip;
            poolAudio.GetComponent<AudioSource>().loop = setting.loop;
            poolAudio.GetComponent<AudioSource>().priority = setting.priority;
            poolAudio.GetComponent<AudioSource>().pitch = setting.pitch;
            poolAudio.GetComponent<AudioSource>().volume = setting.fade ? 0 : volume;
            poolAudio.GetComponent<AudioSource>().spatialBlend = 1;
            poolAudio.GetComponent<AudioSource>().minDistance = minDistance;
            poolAudio.GetComponent<AudioSource>().maxDistance = maxDistance;

            switch(setting.audioType) {
                case AudioType.Sound :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = SFX;
                    break;
                case AudioType.Music :
                    poolAudio.GetComponent<AudioSource>().outputAudioMixerGroup = Music;
                    break;
            }

            poolAudio.GetComponent<AudioSource>().Play();

            if(setting.fade)
            {
                FadeIn(poolAudio.GetComponent<AudioSource>(), volume);
            }
            
            if(!setting.loop && !setting.noneSeamlessly) 
                StartCoroutine(ReleaseSound(clip, poolAudio));
        }

        IEnumerator ReleaseSound(AudioClip clip, GameObject audio) {
            yield return new WaitForSeconds(clip.length);
            Destroy(audio.gameObject);
        }

        public void MuteSound(bool mute, bool fade = false) {
            if(mute)
            {
                if(!fade) GameAudioMixer.SetFloat("SFXVolume", -80);
                else StartFade("SFXVolume");
            }
            else
            { 
                if(!fade) GameAudioMixer.SetFloat("SFXVolume", GameDatas.SoundValue);
                else StopFade("SFXVolume");
            }
        }
        
        void StopFade(string volume)
        {
            isFading = true;

            DOVirtual.Float(-80, volume == "SFXVolume" ? GameDatas.SoundValue : GameDatas.MusicValue, 0.75f, (value) =>
            {
                GameAudioMixer.SetFloat(volume, value);
            }).OnComplete(() => isFading = false);
        }

        void StartFade(string volume)
        {
            isFading = true;

            DOVirtual.Float(volume == "SFXVolume" ? GameDatas.SoundValue : GameDatas.MusicValue, -80, 0.75f, (value) =>
            {
                GameAudioMixer.SetFloat(volume, value);
            }).OnComplete(() => isFading = false);
        }
        

        public void MuteMusic(bool mute, bool fade = false) {
            if(mute)
            {
                if(!fade) GameAudioMixer.SetFloat("MusicVolume", -80);
                else StartFade("MusicVolume");
            }
            else
            { 
                if(!fade) GameAudioMixer.SetFloat("MusicVolume", GameDatas.MusicValue);
                else StopFade("MusicVolume");
            }
        }

        public void MuteMusicAndSoundTemp(bool mute)
        {
            if (mute)
            {
                GameAudioMixer.SetFloat("MusicVolume", -80);
                GameAudioMixer.SetFloat("SFXVolume", -80);
            }
            else
            {
                GameAudioMixer.SetFloat("MusicVolume", GameDatas.MusicValue);
                GameAudioMixer.SetFloat("SFXVolume", GameDatas.SoundValue);
            }
        }

        public void StopSound(SoundName sound) {
            foreach (Transform tp in transform)
            {
                if (dicSound[sound].clip == null) continue;
                if (tp.name == dicSound[sound].clip.name)
                {
                    Destroy(tp.gameObject);
                }
            }
        }

        public void StopSound(AudioClip clip) {

            foreach (Transform tp in transform)
            {
                if (tp.name == clip.name)
                {
                    Destroy(tp.gameObject);
                }
            }
        }

        public void StopSoundFade(AudioClip clip) {
            foreach(Transform tp in transform )
            {
                if(tp.name == clip.name)
                {
                    FadeOut(tp.GetComponent<AudioSource>());
                }
            }
        }

        public void StopSoundFade(SoundName sound)
        {
            foreach (Transform tp in transform)
            {
                if (dicSound[sound].clip == null) continue;
                if (tp.name == dicSound[sound].clip.name)
                {
                    FadeOut(tp.GetComponent<AudioSource>());
                }
            }
        }

        void FadeOut(AudioSource audio)
        {
            isFading = true;
            DOVirtual.Float(audio.volume, 0, 0.75f, (value) =>
            {
                audio.volume = value;
            }).OnComplete(() =>
            {
                isFading = false;
                Destroy(audio.gameObject);
            });
        }

        void FadeIn(AudioSource audio, float volume)
        {
            isFading = true;
            DOVirtual.Float(0, volume, 0.75f, (value) =>
            {
                audio.volume = value;
            }).OnComplete(() =>
            {
                isFading = false;
            });
        }
        
        public void SetMusicValue(float value, bool save = true)
        {
            GameAudioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
            if(save)
                GameDatas.MusicValue = Mathf.Lerp(-80, 0, value);
        }
        public void SetSoundValue(float value, bool save = true)
        {
            GameAudioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, value));
            if(save)
                GameDatas.SoundValue = Mathf.Lerp(-80, 0, value);
        }

        public void MuteAllVolume(bool mute)
        {
            DOVirtual.Float(mute ? 1 : 0, mute ? 0 : 1, 0.5f, (value) =>
            {
                AudioListener.volume = value;
            });
        }
    }
}
