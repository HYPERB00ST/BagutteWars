using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        HandleSceneChange();
    }

    private void HandleSceneChange()
    {
        if (Stats.TimeToPlay <= 0f) {
            SceneManager.LoadScene("ScoreScene");
        }
    }
}
