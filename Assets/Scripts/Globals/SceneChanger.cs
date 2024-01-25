using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class SceneChanger : MonoBehaviour
{
    List<Match> matches = new List<Match>();

    void Update()
    {
        HandleSceneChange();
    }

    private void HandleSceneChange()
    {
        if (Stats.TimeToPlay <= 0f) {
            matches.Add(new Match(Stats.Level, Stats.Points, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            string json = JsonUtility.ToJson(matches.ToArray(), true);
            File.WriteAllText(Application.dataPath + "/matches.json", json);
            SceneManager.LoadScene("ScoreScene");
        }
    }
}
