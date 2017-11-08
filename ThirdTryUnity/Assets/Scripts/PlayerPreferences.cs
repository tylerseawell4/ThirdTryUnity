using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour {
    public int _playerDeathCountToDate;
    public int _playerHighScore;

    // Use this for initialization
    void Start () {
        _playerDeathCountToDate = PlayerPrefs.GetInt("Deaths");
        _playerHighScore = PlayerPrefs.GetInt("HighScore");
    }

    public void SaveDeathCount(int numberOfDeaths)
    {
        _playerDeathCountToDate += numberOfDeaths;
        PlayerPrefs.SetInt("Deaths", _playerDeathCountToDate);
        PlayerPrefs.Save();
    }

    public bool checkHighScore(int currentScore)
    {
        if (currentScore > _playerHighScore || _playerHighScore == 0)
        {
            SaveHighScore(currentScore);
            return true;
        }
        else
        {
            return false;
        }
    }
     
    private void SaveHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
        PlayerPrefs.Save();
    }
}
