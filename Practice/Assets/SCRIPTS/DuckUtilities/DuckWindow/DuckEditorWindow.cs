# if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
#if USE_FACEBOOK
using Facebook.Unity.Settings;
#endif
using UnityEngine.UI;
#if USE_FIREBASE
using Base.Ads;
#endif
using System.IO;
using UnityEditor.Build.Reporting;

namespace DuckGame.Ultilities
{
    public enum SETTINGTYPE { GameSettings, BuildSettings }
    public enum APILEVEL { HIGHEST, CUSTOM }
    public enum QUALITY { VERYLOW, LOW, MEDIUM, HIGH, VERYHIGH, ULTRA }
    public enum UNLOCKALL { NO, YES }
    public enum VIBRATION { NO, YES }

    public class DuckUnityWindow : OdinEditorWindow
    {
        private static DuckUnityWindow myWindow;

        [MenuItem("DuckGame/Settings")]
        public static void ShowWindow()
        {
            myWindow = GetWindow<DuckUnityWindow>("DuckGame");
            myWindow.position = new Rect(new Vector2(627, 174), new Vector2(500, 750));
        }

        [MenuItem("DuckGame/UnityDeveloperMode")]
        public static void UnityDeveloperMode()
        {
            EditorPrefs.SetBool("DeveloperMode", !EditorPrefs.GetBool("DeveloperMode", false));
            DuckHelper.LogEditor("Developer Mode Enable !!!!");
        }

        [HideLabel]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        public Sprite Icon;

        string platform;
        [TitleGroup("$platform", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [EnumToggleButtons, HideLabel] public SETTINGTYPE settingType;

        [ShowIf("settingType", SETTINGTYPE.GameSettings)]
        [ProgressBar(0, 1000000, r: 0, g: 1, b: 0, Height = 30)]
        [BoxGroup(" ")]
        public int Money;

        [ShowIf("settingType", SETTINGTYPE.GameSettings)]
        [ProgressBar(0, 100000, r: 0, g: 1, b: 1, Height = 30)]
        [BoxGroup(" ")]
        public int Distance;

        [ShowIf("settingType", SETTINGTYPE.GameSettings)]
        [EnumToggleButtons]
        [BoxGroup(" ")]
        public UNLOCKALL UnlockAllItems;

        [ShowIf("settingType", SETTINGTYPE.GameSettings)]
        [BoxGroup(" ")]
        [Button(ButtonSizes.Medium)] [GUIColor(1,0,0)]
        void CLEARDATAS()
        {

        }


        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [BoxGroup(" ")]
        [PreviewField(100, ObjectFieldAlignment.Left)]
        public Texture2D GameIcon;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [GUIColor(0, 1, 0)]
        [BoxGroup(" ")]
        public string GameName;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [GUIColor(1, 1, 0)]
        [BoxGroup(" ")]
        public string PackageName;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [BoxGroup(" ")]
        public UIOrientation Orientation;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [BoxGroup(" ")]
        public string GameVersion;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [BoxGroup(" ")]
        public int BundleVersion;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [EnumToggleButtons]
        [BoxGroup(" ")]
        public APILEVEL TargetAPI;

        [ShowIf("@this.settingType == SETTINGTYPE.BuildSettings && this.TargetAPI == APILEVEL.CUSTOM")]
        [BoxGroup(" ")]
        public AndroidSdkVersions API;
        [ShowIf("settingType", SETTINGTYPE.BuildSettings)]
        [GUIColor(0, 1, 1)]
        [BoxGroup(" ")]
        public QUALITY GameQuality;
 

        protected override void Initialize()
        {
            InitSetting();
        }

        void InitSetting()
        {
            platform = "----- " + EditorUserBuildSettings.activeBuildTarget.ToString() + " -----";

            GameIcon = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown)[0];
            GameName = PlayerSettings.productName;
            PackageName = PlayerSettings.applicationIdentifier;
            Orientation = PlayerSettings.defaultInterfaceOrientation;
            GameVersion = PlayerSettings.bundleVersion;
            BundleVersion = PlayerSettings.Android.bundleVersionCode;
            if (PlayerSettings.Android.targetSdkVersion == AndroidSdkVersions.AndroidApiLevelAuto)
                TargetAPI = APILEVEL.HIGHEST;
            else
            {
                TargetAPI = APILEVEL.CUSTOM;
                API = PlayerSettings.Android.targetSdkVersion;
            }

            QUALITY result = (QUALITY)QualitySettings.GetQualityLevel();
            GameQuality = result;


            //Money = GameDatas.Money;
            //Distance = GameDatas.Distance;

        }

        void SaveSetting()
        {

            if (GameIcon != PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown)[0])
                PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new Texture2D[] { GameIcon });

            if (PlayerSettings.productName != GameName)
                PlayerSettings.productName = GameName;

            if (PlayerSettings.applicationIdentifier != PackageName)
                PlayerSettings.applicationIdentifier = PackageName;

            if (PlayerSettings.defaultInterfaceOrientation != Orientation)
                PlayerSettings.defaultInterfaceOrientation = Orientation;

            if (PlayerSettings.bundleVersion != GameVersion)
                PlayerSettings.bundleVersion = GameVersion;

            if (PlayerSettings.Android.bundleVersionCode != BundleVersion)
                PlayerSettings.Android.bundleVersionCode = BundleVersion;

            if (TargetAPI == APILEVEL.HIGHEST)
            {
                if (PlayerSettings.Android.targetSdkVersion != AndroidSdkVersions.AndroidApiLevelAuto)
                    PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            }
            else
            {
                if (PlayerSettings.Android.targetSdkVersion != API)
                    PlayerSettings.Android.targetSdkVersion = API;
            }

            if (QualitySettings.GetQualityLevel() != (int)GameQuality)
                QualitySettings.SetQualityLevel((int)GameQuality);
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            SaveSetting();
        }

    }
}
#endif
