using UnityEngine;
namespace DuckGame.Ultilities
{
public class GameDatas
{
	public static float MusicValue
	{
		get { return PlayerPrefs.GetFloat("MusicValue", 1); }
		set { PlayerPrefs.SetFloat("MusicValue", value); }
	}
	public static float SoundValue
	{
		get { return PlayerPrefs.GetFloat("SoundValue", 1); }
		set { PlayerPrefs.SetFloat("SoundValue", value); }
	}
	public static string FirstTimeDate
	{
		get { return PlayerPrefs.GetString("FirstTimeDate", ""); }
		set { PlayerPrefs.SetString("FirstTimeDate", value); }
	}
	public static string LastTimeDate
	{
		get { return PlayerPrefs.GetString("LastTimeDate", ""); }
		set { PlayerPrefs.SetString("LastTimeDate", value); }
	}
	public static string FirstTimeInitData
	{
		get { return PlayerPrefs.GetString("FirstTimeInitData", "false"); }
		set { PlayerPrefs.SetString("FirstTimeInitData", value); }
	}
	public static int Money
	{
		get { return PlayerPrefs.GetInt("Money", 0); }
		set { PlayerPrefs.SetInt("Money", value); }
	}
	public static int Diamond
	{
		get { return PlayerPrefs.GetInt("Diamond", 0); }
		set { PlayerPrefs.SetInt("Diamond", value); }
	}
	public static string LevelDatas
	{
		get { return PlayerPrefs.GetString("LevelDatas", ""); }
		set { PlayerPrefs.SetString("LevelDatas", value); }
	}
	public static string VehicleDatas
	{
		get { return PlayerPrefs.GetString("VehicleDatas", ""); }
		set { PlayerPrefs.SetString("VehicleDatas", value); }
	}
	public static string DictionaryDatas
	{
		get { return PlayerPrefs.GetString("DictionaryDatas", ""); }
		set { PlayerPrefs.SetString("DictionaryDatas", value); }
	}
	public static string OtherDatas
	{
		get { return PlayerPrefs.GetString("OtherDatas", ""); }
		set { PlayerPrefs.SetString("OtherDatas", value); }
	}
}
}
