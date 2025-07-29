#if USING_FIREBASE
using System.Collections.Generic;
using DuckGame.Ultilities;
using UnityEngine;

public static class FirebaseLog
{
    public static void LogPlayCount(leveldata levelData)
    {
        if (DuckGame.Ultilities.DataManager.Instance.OtherDatas.PlayCount > 10) return;
        FirebaseManager.LogEvent("x_play_start_count_" + DuckGame.Ultilities.DataManager.Instance.OtherDatas.PlayCount, new Dictionary<string, object>
        {
            {"", (levelData.Index +1 ).ToString()}
        });        
    }
    public static void LogMessage(string message)
    {
        FirebaseManager.LogEvent(message);
    }
    public static void LogNativeBannerClick(string position)
    {
        FirebaseManager.LogEvent("ad_native_banner_click", new Dictionary<string, object>
        {
            {"position", position}
        });
    }
    public static void LogLevelStartAll(leveldata levelData)
    {
        FirebaseManager.LogEvent("x_level_start_all", new Dictionary<string, object>
        {
            {"level", (levelData.Index + 1).ToString()}
        });
    }
    public static void LoglevelWinAll(leveldata levelData)
    {
        FirebaseManager.LogEvent("x_level_win_all", new Dictionary<string, object>
        {
            {"level", (levelData.Index + 1).ToString()}
        });
    }
    public static void LoglevelFailAll(leveldata levelData)
    {
        FirebaseManager.LogEvent("x_level_failed_all", new Dictionary<string, object>
        {
            {"level", (levelData.Index + 1).ToString()}
        });
    }
    public static void LogLevelReviveAll(leveldata levelData)
    {
        FirebaseManager.LogEvent("x_level_revive_all", new Dictionary<string, object>
        {
            {"level", (levelData.Index + 1).ToString()}
        });
    }
    public static void LoglevelStart(leveldata levelData)
    {
        if (levelData.Index > 9) return;
        FirebaseManager.LogEvent("x_level_start_" + (levelData.Index + 1).ToString(), new Dictionary<string, object>
        {
            {"car",DuckGame.Ultilities.DataManager.Instance.vehicleDynamicData.CurVehicleData.vehicleName}
        });
    }
    public static void LogLevelWin(leveldata levelData)
    {
        if (levelData.Index > 9) return;
        FirebaseManager.LogEvent("x_level_win_" + (levelData.Index + 1).ToString(), new Dictionary<string, object>
        {
            {"car",DuckGame.Ultilities.DataManager.Instance.vehicleDynamicData.CurVehicleData.vehicleName}
        });
    }
    public static void LogLevelFail(leveldata levelData)
    {
        if (levelData.Index > 9) return;
        FirebaseManager.LogEvent("x_level_fail_" + (levelData.Index + 1).ToString(), new Dictionary<string, object>
        {
            {"car",DuckGame.Ultilities.DataManager.Instance.vehicleDynamicData.CurVehicleData.vehicleName}
        });
    }
    public static void LogLevelRevive(leveldata levelData)
    {
        if (levelData.Index > 9) return;
        FirebaseManager.LogEvent("x_level_revive_" + (levelData.Index + 1).ToString(), new Dictionary<string, object>
        {
            {"car",DuckGame.Ultilities.DataManager.Instance.vehicleDynamicData.CurVehicleData.vehicleName}
        });
    }
    public static void LogEarnvitualCurrency(int value, string name)
    {
        FirebaseManager.LogEvent("virtual_currency_earn", new Dictionary<string, object>
        {
            {name,value}
        });
    }
    public static void LogSpendvitualCurrency(int value, string name)
    {
        FirebaseManager.LogEvent("virtual_currency_spend", new Dictionary<string, object>
        {
            {name,value}
        });
    }
}
#endif