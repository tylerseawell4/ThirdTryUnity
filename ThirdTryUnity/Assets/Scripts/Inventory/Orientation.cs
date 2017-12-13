using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orientation : MonoBehaviour {
    Scene _scene;
    // Use this for initialization
    void Start () {
        _scene = SceneManager.GetActiveScene();
        if (_scene.buildIndex == 3)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

    }

    // Update is called once per frame
    void Update () {
     //   Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
