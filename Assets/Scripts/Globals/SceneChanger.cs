using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start() {
        Application.targetFrameRate = 60;
    }
   
    // Update is called once per frame
    void Update()
    {
        HandleSceneChange();
    }

    private void HandleSceneChange()
    {
        if (Stats.PlayTime <= 0f) {
            TakeScreenshot();
            SceneManager.LoadScene("ScoreScene");
        }
    }

    private void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Assets/Endscreen.png");
    }
}
