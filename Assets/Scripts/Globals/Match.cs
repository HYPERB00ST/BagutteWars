using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    public int level;
    public int score;
    public string date;

    public Match(int level, int score, string date)
    {
        this.level = level;
        this.score = score;
        this.date = date;
    }
}

// TO USE THIS YOU HAVE TO MAKE A NEW INSTANCE OF THE CLASS AS LIST
// List<Match> matches = new List<Match>(); ON THE START FUNCTION ON THE SCRIPT FOR THE GAMEMANAGER

// THEN YOU CAN ADD MATCHES TO THE LIST LIKE THIS
// matches.Add(new Match(Stats.Level, Stats.Points, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

// THEN YOU CAN SAVE THE LIST TO A JSON FILE LIKE THIS
// string json = JsonHelper.ToJson(matches.ToArray(), true);
// File.WriteAllText(Application.dataPath + "/matches.json", json);

// YOU CAN LOAD THE LIST FROM THE JSON FILE LIKE THIS
// string json = File.ReadAllText(Application.dataPath + "/matches.json");
// matches = JsonHelper.FromJson<Match>(json);

// YOU CAN PRINT THE LIST LIKE THIS
// foreach (Match match in matches)
// {
//     Debug.Log(match.level + " " + match.score + " " + match.date);
// }