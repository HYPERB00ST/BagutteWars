using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private string StatsGameobjectName = "Stats";
    Stats stats;
    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        stats = GameObject.Find(StatsGameobjectName).GetComponent<Stats>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "ScoreScene") {
            stats.inPlay = false;
        }
        else if (scene.name == "PlayScene") {
            stats.inPlay = true;
        }
    }
}
