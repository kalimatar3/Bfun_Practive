using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorUpgrade
{
    public string colorName;
    //public Sprite colorIcon;
    public Color color;
    //public Color colorDark;
}
[System.Serializable]
public struct SkinUpgrade
{
    public string skinName;
    public Texture2D skin;
}

public enum REQUIRETYPE { TriggerHit, TriggerHitCount, Point}
public enum HITCOUNTTYPE { Ragdoll, Container }
public enum AIDIFFICULT { Lose = 0, Easy = 1, Normal = 2, Hard = 3, VeryHard = 4}

[Serializable]
public struct DecalData
{
    public Texture decalTexture2D;
    public Vector2 decalPos;
    public float scaleDecal;
    public float rotateDecal;
    public float flipX;
    public float flipY;
}

[Serializable]
public struct ObjectIcon
{
    public string objectName;
    public Sprite objectIcon;
}

[Serializable]
public struct AiRaceData
{
    public AIDIFFICULT aiDifficult;
    public AnimationCurve[] speedMulCurve;
}

public enum LANEDIRECTION { Right, Left }
public enum TIME { Day, Night }
public enum LEVELMODE { RaceAI, PerfectLap, PolicePursuit}
public enum CHARACTER { None, Leo, Kat, Victor, Elena, Raul, Max}

[Serializable]
public struct LevelInfoData
{
    public string sceneName;
    public LEVELMODE levelMode;
    public GameObject levelPrefab;
    public float levelTime;
    public LANEDIRECTION laneDirection;
    public TIME timeRace;
    public int numberOfAI;
    public CHARACTER firstCharacterName;
    public CHARACTER secondCharacterName;
    public CHARACTER thirdCharacterName;
    public string levelDecs;
    public REWARDTYPE levelReward;
    public int moneyReward;
    public int tokenReward;
    public int ticketReward;
}

[Serializable]
public struct ChapterData
{
    public string chapterName;
    public CHARACTER chapterCharacter;
    public LevelInfoData[] levelInfoDatas;
    public string chapterIntro1, chapterIntro2;
    public Reward[] chapterRewards;
}

[Serializable]
public struct AllChapter
{
    public LevelInfoData levelTutorial;
    public ChapterData[] racingChapter;
}

namespace Clouds.Ultilities
{
    [HideMonoScript]
    public class DataManager : Singleton<DataManager>
    {
        //         [HideLabel]
        //         [PreviewField(100, ObjectFieldAlignment.Center)]
        //         public Sprite Icon;

        //         [TitleGroup("DATA MANAGER", "@DuckGame", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        //         [InlineEditor(InlineEditorModes.GUIAndPreview)] public VehicleUnlockExcelDatas vehicleUnlockData;
        //         public Dictionary<string, VehicleUnlockInfo> vehicleUnlockDics = new Dictionary<string, VehicleUnlockInfo>();
        //         [InlineEditor(InlineEditorModes.GUIAndPreview)] public VehicleAdvanceUpgradeExcelDatas vehicleAdvanceUpgradeData;
        //         [SerializeField] ColorUpgrade[] allColors;
        //         [SerializeField] SkinUpgrade[] allSkins;
        //         public Dictionary<string, VehicleAdvanceUpgradeInfo> RimUpgradeDics = new Dictionary<string, VehicleAdvanceUpgradeInfo>();
        //         public Dictionary<string, VehicleAdvanceUpgradeInfo> TireUpgradeDics = new Dictionary<string, VehicleAdvanceUpgradeInfo>();
        //         public Dictionary<string, VehicleAdvanceUpgradeInfo> ColorupgradeDics = new Dictionary<string, VehicleAdvanceUpgradeInfo>();
        //         public Dictionary<string, VehicleAdvanceUpgradeInfo> SkinupgradeDics = new Dictionary<string, VehicleAdvanceUpgradeInfo>();
        //         public Dictionary<string, ColorUpgrade> allColorDics = new Dictionary<string, ColorUpgrade>();
        //         public Dictionary<string, SkinUpgrade> allSkinDics = new Dictionary<string, SkinUpgrade>();
        //         internal static bool ISCOMPLETEDLOADDATA;
        //         public LevelSO levelSO;
        //         public LevelDynamicData levelDynamicData;
        //         public VehicleDynamicData vehicleDynamicData;
        //         public CurrencyDynamicData CurrencyDatas;
        //         public OtherData OtherDatas;
        //         public Dictionary<string, VehicleData> vehicleDataDics = new Dictionary<string, VehicleData>();
        // #if UNITY_EDITOR
        //         [ButtonGroup]
        //         public void SAVEDATA()
        //         {
        //             this.SaveCurrencyDatas();
        //         }
        // #endif
                public override void Awake()
                {
                    base.Awake();
                    this.LoadData();
                }

        //         private void InitHardData()
        //         {
        //             for (int i = 0; i < vehicleUnlockData.Sheet1.Count; i++)
        //             {
        //                 vehicleUnlockDics.Add(vehicleUnlockData.Sheet1[i].vehicleName, vehicleUnlockData.Sheet1[i]);
        //             }
        //             for (int i = 0; i < vehicleAdvanceUpgradeData.Sheet1.Count; i++)
        //             {
        //                 if (vehicleAdvanceUpgradeData.Sheet1[i].upgradeType == UPGRADETYPE.Rim)
        //                     RimUpgradeDics.Add(vehicleAdvanceUpgradeData.Sheet1[i].upgradeName, vehicleAdvanceUpgradeData.Sheet1[i]);
        //                 else if (vehicleAdvanceUpgradeData.Sheet1[i].upgradeType == UPGRADETYPE.Tire)
        //                 {
        //                     TireUpgradeDics.Add(vehicleAdvanceUpgradeData.Sheet1[i].upgradeName, vehicleAdvanceUpgradeData.Sheet1[i]);
        //                 }
        //                 else if (vehicleAdvanceUpgradeData.Sheet1[i].upgradeType == UPGRADETYPE.Color)
        //                 {
        //                     ColorupgradeDics.Add(vehicleAdvanceUpgradeData.Sheet1[i].upgradeName, vehicleAdvanceUpgradeData.Sheet1[i]);
        //                 }
        //                 else if (vehicleAdvanceUpgradeData.Sheet1[i].upgradeType == UPGRADETYPE.Skin)
        //                 {
        //                     SkinupgradeDics.Add(vehicleAdvanceUpgradeData.Sheet1[i].upgradeName, vehicleAdvanceUpgradeData.Sheet1[i]);
        //                 }
        //             }
        //             for (int i = 0; i < allColors.Length; i++)
        //             {
        //                 allColorDics.Add(allColors[i].colorName, allColors[i]);
        //             }
        //             for (int i = 0; i < allSkins.Length; i++)
        //             {
        //                 allSkinDics.Add(allSkins[i].skinName, allSkins[i]);
        //             }
        //         }
        [SerializeField] LevelDataSO levelDataSO;
        public LevelDynamicData levelDynamicData;
        private void LoadData()
        {
            StartCoroutine(ILoadData());
        }
        private IEnumerator ILoadData()
        {
            yield return new WaitUntil(() =>
            {
                if (!GameLS.LoadDataFromJson<LevelDynamicData>(GameDatas.LevelDatas, InitLevelDatas(), out levelDynamicData)) return false;
                return true;
            });
        }
        private LevelDynamicData InitLevelDatas()
        {
            LevelDynamicData levelDynamicData = new LevelDynamicData();
            levelDynamicData.levelDatas = levelDataSO.levelDatas;
            return levelDynamicData;
        }
        //         protected LevelDynamicData InitLevelDatas()
        //         {
        //             List<leveldata> levelDatas = new List<leveldata>();
        //             for (int i = 0; i < levelSO.hardLevelDatas.Count; i++)
        //             {
        //                 leveldata levelData = new leveldata(levelSO.hardLevelDatas[i]);
        //                 levelData.Locked = false;
        //                 levelDatas.Add(levelData);
        //             }
        //             LevelDynamicData levelDynamicData = new LevelDynamicData(levelDatas);
        //             return levelDynamicData;
        //         }
        //         protected CurrencyDynamicData InitCurrencyDatas()
        //         {
        //             CurrencyDynamicData currencyDynamicDatas = new CurrencyDynamicData();
        //             return currencyDynamicDatas;
        //         }
        //         protected VehicleDynamicData InitvehicleData()
        //         {
        //             List<VehicleData> listvehicledata = new List<VehicleData>();
        //             VehicleData vehicleData = new VehicleData(vehicleUnlockDics["2018_Nexis_V5_Vehicle"]);
        //             listvehicledata.Add(vehicleData);
        //             VehicleDynamicData vehicleDynamicData = new VehicleDynamicData(listvehicledata);
        //             vehicleDynamicData.CurVehicleData = vehicleData;
        //             return vehicleDynamicData;
        //         }
        //         public void SaveLevelDatas()
        //         {
        //             GameDatas.LevelDatas = LSManager.SaveGameby<LevelDynamicData>(levelDynamicData);
        //         }
        //         public void SaveVehicleDatas()
        //         {
        //             GameDatas.VehicleDatas = LSManager.SaveGameby<VehicleDynamicData>(vehicleDynamicData);
        //         }
        //         public void SaveOtherDatas()
        //         {
        //             GameDatas.OtherDatas = LSManager.SaveGameby<OtherData>(OtherDatas);
        //         }
        //         public void CollectVehicle(VehicleUnlockInfo vehicleUnlockInfo)
        //         {
        //             if (vehicleDataDics.ContainsKey(vehicleUnlockInfo.vehicleName)) return;
        //             VehicleData vehicleData = new VehicleData(vehicleUnlockInfo);
        //             vehicleDynamicData.OwnedVehicles.Add(vehicleData);
        //             vehicleDataDics.Add(vehicleUnlockInfo.vehicleName, vehicleData);
        //             SaveVehicleDatas();
        //         }
        //         public void SaveCurrencyDatas()
        //         {
        //             GameDatas.DictionaryDatas = LSManager.SaveGameConvertby<CurrencyDynamicData>(CurrencyDatas);
        //         }
        //     [InlineEditor(InlineEditorModes.GUIAndPreview)] public VehicleUnlockExcelDatas vehicleUnlockData;
        //     [ReadOnly][InlineEditor(InlineEditorModes.GUIAndPreview)] public VehicleAdvanceUpgradeExcelDatas vehicleAdvanceUpgradeData;
        //     [ReadOnly][InlineEditor(InlineEditorModes.GUIAndPreview)] public DailyLoginExcelDatas dailyLoginExcelDatas;
        //     [ReadOnly][InlineEditor(InlineEditorModes.GUIAndPreview)] public VehicleStatsExcelDatas vehicleStatsExcelDatas;
        //     [ReadOnly][InlineEditor(InlineEditorModes.GUIAndPreview)] public AchievmentExcelDatas achievmentExcelDatas;
        //     [ReadOnly][InlineEditor(InlineEditorModes.GUIAndPreview)] public DailyMissionExcelDatas dailyMissionExcelDatas;
        //     public AllChapter allChapter;

        //     [SerializeField] ObjectIcon[] vehicleIcons;
        //     [SerializeField] ObjectIcon[] rimIcons;
        //     [SerializeField] ObjectIcon[] characterIcons;
        //     [SerializeField] ObjectIcon[] characterBigIcon;

        //     public string[] aiCarName;
        //     [SerializeField] AiRaceData[] aiRaceDatas;

        //     [HideInInspector] public VehicleUpgrade currentVehicleUpgrade;
        //     public ColorUpgrade[] allColors;
        //     public GameObject lightLens;

        //     float unitConversion = 1.0f;

        //     public Dictionary<string, ColorUpgrade> allColorDics = new Dictionary<string, ColorUpgrade>();
        //     public Dictionary<string, ObjectIcon> allVehicleIconDics = new Dictionary<string, ObjectIcon>();
        //     public Dictionary<string, ObjectIcon> allRimIconDics = new Dictionary<string, ObjectIcon>();
        //     public Dictionary<string, ObjectIcon> allCharIcon = new Dictionary<string, ObjectIcon>();
        //     public Dictionary<string, ObjectIcon> allCharBigIcon = new Dictionary<string, ObjectIcon>();
        //     public Dictionary<string, Sprite> allVehicleBrandIconDics = new Dictionary<string, Sprite>();
        //     public Dictionary<string, Sprite> allDecalIconDics = new Dictionary<string, Sprite>();
        //     public Dictionary<AIDIFFICULT, AnimationCurve[]> aiRaceDics = new Dictionary<AIDIFFICULT, AnimationCurve[]>();

        //     public const int RANKCMAX = 599, RANKBMAX = 749, RANKAMAX = 849, RANKSMAX = 999, RANKSPLUSMAX = 1150;

        //     public bool unlockAll;

        //     int aiDifficultIndex;

        //     public AIDIFFICULT AIDifficult
        //     {
        //         get { return (AIDIFFICULT)aiDifficultIndex; }
        //     }

        //     public AnimationCurve[] currentAIDifficult
        //     {
        //         get { return aiRaceDics[(AIDIFFICULT)aiDifficultIndex]; }
        //     }


        //     public DateTime FirstTimeLoginDate
        //     {
        //         get {
        //             return DateTime.Parse(GameDatas.FirstTimeDate);
        //         }
        //     }

        //     public int CurrentDayAfterFirstTimeLoginDate
        //     {
        //         get {
        //             return DateTime.Today.Subtract(FirstTimeLoginDate).Days + 1;
        //         }
        //     }

        //     public bool PassAnotherday
        //     {
        //         get {
        //             return DateTime.Today.Subtract(DateTime.Parse(GameDatas.LastTimeDate == string.Empty ? GameDatas.FirstTimeDate : GameDatas.LastTimeDate)).Days > 0;
        //         }
        //     }

        //     public bool HasClaimedDailyToday
        //     {
        //         get
        //         {
        //             return PlayerPrefs.GetString("DailyClaimed" + CurrentDayAfterFirstTimeLoginDate, "false") == "true";
        //         }
        //     }

        //     public int TotalCars
        //     {
        //         get
        //         {
        //             return vehicleUnlockData.Sheet1.Count;
        //         }
        //     }

        //     public int TotalCarsUnlock
        //     {
        //         get
        //         {
        //             return vehicleUnlockData.Sheet1.Where(s => PlayerPrefs.GetString(s.vehicleName + "Unlock") == "true").ToArray().Length;
        //         }
        //     }

        //     public override void Awake()
        //     {
        //         base.Awake();
        //         InitData();
        //     }

        //     void InitData()
        //     {

        //         for(int i = 0; i < dailyLoginExcelDatas.Sheet1.Count; i++)
        //         {
        //             dailyLoginExcelDatas.dailyLoginDatasDic.Add(dailyLoginExcelDatas.Sheet1[i].day, dailyLoginExcelDatas.Sheet1[i]);
        //         }

        //         for(int i = 0; i < vehicleUnlockData.Sheet1.Count; i++)
        //         {
        //             if (GameDatas.FirstTimeInitData == "false")
        //             {
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + "Color", vehicleUnlockData.Sheet1[i].defaultColor);
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + vehicleUnlockData.Sheet1[i].defaultColor + "Unlock", "true");
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + "Decal", vehicleUnlockData.Sheet1[i].defaultDecal);
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + vehicleUnlockData.Sheet1[i].defaultDecal + "Unlock", "true");
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + "Neon", vehicleUnlockData.Sheet1[i].defaultNeon);
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + vehicleUnlockData.Sheet1[i].defaultNeon + "Unlock", "true");
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + "Rim", vehicleUnlockData.Sheet1[i].defaultRim);
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + vehicleUnlockData.Sheet1[i].defaultRim + "Unlock", "true");
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + "Tire", vehicleUnlockData.Sheet1[i].defaultTire);
        //                 PlayerPrefs.SetString(vehicleUnlockData.Sheet1[i].vehicleName + vehicleUnlockData.Sheet1[i].defaultTire + "Unlock", "true");
        //                 PlayerPrefs.SetInt(vehicleUnlockData.Sheet1[i].vehicleName + "CurrentTopSpeedUpgrade", 1);
        //                 PlayerPrefs.SetInt(vehicleUnlockData.Sheet1[i].vehicleName + "CurrentAccelerationUpgrade", 1);
        //                 PlayerPrefs.SetInt(vehicleUnlockData.Sheet1[i].vehicleName + "CurrentHandlingUpgrade", 1);
        //                 PlayerPrefs.SetInt(vehicleUnlockData.Sheet1[i].vehicleName + "CurrentNitroUpgrade", 1);
        //             }
        //         }

        //         for (int i = 0; i < vehicleUnlockData.Sheet1.Count; i++)
        //         {
        //             vehicleUnlockData.vehicleUnlockDics.Add(vehicleUnlockData.Sheet1[i].vehicleName, vehicleUnlockData.Sheet1[i]);
        //         }

        //         var vehicleNameCounts = new Dictionary<string, int>();

        //         foreach (VehicleStatInfo vehicleStat in vehicleStatsExcelDatas.Sheet1)
        //         {
        //             if (vehicleNameCounts.ContainsKey(vehicleStat.stageName))
        //             {   
        //                 vehicleNameCounts[vehicleStat.stageName]++;
        //                 vehicleStatsExcelDatas.vehicleStatsDics.Add(vehicleStat.stageName + vehicleNameCounts[vehicleStat.stageName], vehicleStat);
        //             }
        //             else
        //             {
        //                 vehicleNameCounts[vehicleStat.stageName] = 1;
        //                 vehicleStatsExcelDatas.vehicleStatsDics.Add(vehicleStat.stageName + 1, vehicleStat);
        //             }
        //         }

        //         if (GameDatas.FirstTimeInitData == "false")
        //         {
        //             foreach (var kvp in vehicleNameCounts)
        //             {
        //                 PlayerPrefs.SetInt(kvp.Key + "MaxUpgrade", kvp.Value);
        //             }
        //         }

        //         foreach(AchievmentInfo ai in achievmentExcelDatas.Sheet1)
        //         {
        //             achievmentExcelDatas.achievmentDatasDic.Add(ai.title, ai);
        //         }

        //         foreach(DailyMissionInfo dm in dailyMissionExcelDatas.Sheet1)
        //         {
        //             dailyMissionExcelDatas.dailyMissionDatasDic.Add(dm.title, dm);
        //         }

        //         foreach(ObjectIcon vehicleIcon in vehicleIcons)
        //         {
        //             allVehicleIconDics.Add(vehicleIcon.objectName, vehicleIcon);
        //         }

        //         foreach (ObjectIcon rimIcon in rimIcons)
        //         {
        //             allRimIconDics.Add(rimIcon.objectName, rimIcon);
        //         }

        //         foreach (ObjectIcon charIcon in characterIcons)
        //         {
        //             allCharIcon.Add(charIcon.objectName, charIcon);
        //         }

        //         foreach (ObjectIcon charIcon in characterBigIcon)
        //         {
        //             allCharBigIcon.Add(charIcon.objectName, charIcon);
        //         }

        //         if (GameDatas.FirstTimeInitData == "false")
        //         {
        //             GameDatas.FirstTimeDate = DateTime.Today.ToShortDateString();
        //             DuckHelper.LogGame("FIRST TIME LOGIN : " + GameDatas.FirstTimeDate);
        //             GameDatas.FirstTimeInitData = "true";
        //         }

        //         for(int i = 0; i < vehicleAdvanceUpgradeData.Sheet1.Count; i++)
        //         {
        //             vehicleAdvanceUpgradeData.vehicleAdvanceUpgradeDics.Add(vehicleAdvanceUpgradeData.Sheet1[i].upgradeName, vehicleAdvanceUpgradeData.Sheet1[i]);
        //         }

        //         foreach(ColorUpgrade colorUpgrade in allColors)
        //         {
        //             allColorDics.Add(colorUpgrade.colorName, colorUpgrade);
        //         }

        //         foreach(AiRaceData aiRaceData in aiRaceDatas)
        //         {
        //             aiRaceDics.Add(aiRaceData.aiDifficult, aiRaceData.speedMulCurve);
        //         }
        //     }

        //     public void UpDifficult()
        //     {
        //         if(aiDifficultIndex < 4)
        //             aiDifficultIndex++;
        //     }

        //     public void DownDifficult()
        //     {
        //         if (aiDifficultIndex > 0)
        //             aiDifficultIndex--;
        //     }

        //     public void ResetDifficult()
        //     {
        //         aiDifficultIndex = 0;
        //     }

        //     private void OnDestroy() {
        //         GameDatas.LastTimeDate = DateTime.Today.ToShortDateString();
        //     }
    }
}
