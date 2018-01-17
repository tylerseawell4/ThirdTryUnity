using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour {
    public int _playerDeathCountToDate;
    public int _playerHighScore;
    public string _doojeeSuit;


    private GameObject _doojee;
    private SpriteRenderer _doojeeSprite;
    private string _doojeeSuitName;
    // Use this for initialization
    void Start () {
       //PlayerPrefs.DeleteAll();
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

    public void SaveDoojeeSuit()
    {
        _doojee = GameObject.Find("Body");
        if (_doojee != null)
        {
            _doojeeSprite = _doojee.GetComponent<SpriteRenderer>();
            _doojeeSuitName = _doojeeSprite.sprite.name;

            FindObjectOfType<Purchaser>().BuyNonConsumable(_doojeeSuitName);
        }
        PlayerPrefs.SetString("DoojeeSuit", _doojeeSuitName);
        _doojeeSuit = _doojeeSuitName;
    }
}
