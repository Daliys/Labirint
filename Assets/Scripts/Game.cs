using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool isEnableSound;
    public static bool isEnableFullScreen;
    public static TypeOfDifficulties typeOfDifficulties;
    public static int bestScore;
    public static int currestScore;
    public enum TypeOfDifficulties
    {
        Easy, Normal, Hard, Speed
    }

    void Awake()
    {
        LoadAllData();
        DontDestroyOnLoad(this);
    }

    private void LoadAllData()
    {
        isEnableSound = GetPreference("isEnableSound", 1) == 1 ? true : false;
        isEnableFullScreen = GetPreference("isEnableFullScreen", 1) == 1 ? true : false;
        bestScore = GetPreference("bestScore", 0);
    }


    private static void SavePreference(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private static int GetPreference(string key, int defoultValue)
    {
        if (!PlayerPrefs.HasKey(key)) return defoultValue;
        return PlayerPrefs.GetInt(key);
    }

    public static void SaveSoundValue(bool value)
    {
        isEnableSound = value;
        if (value) SavePreference("isEnableSound", 1); 
        else SavePreference("isEnableSound", 0);
    }

    public static void SaveFullScreenValue(bool value)
    {
        isEnableFullScreen = value;
        if (value) SavePreference("isEnableFullScreen", 1);
        else SavePreference("isEnableFullScreen", 0);
    }

    public static void SaveScore()
    {
        if(currestScore > bestScore)
        {
            bestScore = currestScore;
            SavePreference("bestScore", currestScore);
            currestScore = 0;
        }
    }

}
