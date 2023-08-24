using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{

    public const string playePrefKey_AutoSave = "autosaveprogress";

    // Generic Methods PlayerPrefs
    public static string GetPlayerPrefs(string _key)
    {
        return PlayerPrefs.GetString(_key);
    }
    // for returning default value
    public static int GetPlayerPrefs(string _key, int _value)
    {
        return PlayerPrefs.GetInt(_key, _value);
    }

    public static void SetPlayerPrefs(string _key, int _value)
    {
        PlayerPrefs.SetInt(_key, _value);
        PlayerPrefs.Save();
    }

    public static void SetAutoSavePregression(int value)
    {
        SetPlayerPrefs(playePrefKey_AutoSave, value);
    }
    public static bool IsAutoSavedEnable()
    {
        return (GetPlayerPrefs(playePrefKey_AutoSave, 0) > 0) ? true : false;
    }
}