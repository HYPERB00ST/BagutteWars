using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float StartingTomeToPlay = 60f;
    [SerializeField] private float SecondsPerToast = 6f;

    // Static stats
    internal static float TimeToPlay;
    internal static int Points {get; private set;} = 0;
    internal static float TotalTimePassed {get; private set;} = 0f;
    internal static int Level {get; private set;} = 1;

    // Flag for scene change
    internal bool inPlay = true;

    void Start() {
        TimeToPlay = StartingTomeToPlay;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (!inPlay) {
            return;
        }
        TimerUpdate();
        LevelUpdate();
        TimeToPlayUpdate();
    }

    private void TimeToPlayUpdate()
    {
        TimeToPlay -= Time.deltaTime;
    }

    private void TimerUpdate()
    {
        TotalTimePassed += Time.deltaTime;
    }

    void ResetTimer() {
        // TODO: Call on main menu
        TotalTimePassed = 0f;
    }

    void ResetScore() {
        // TODO: Call on main menu
    }

    private void LevelUpdate()
    {
        Level = (int)TotalTimePassed / 10;
        if (Level < 1) Level = 1;
    }

    internal void AddPoint() {
        Points += 10;
        ExtendTimeToPlay();
    }

    private void ExtendTimeToPlay()
    {
        TimeToPlay += SecondsPerToast;
    }

    internal void ShortenTimeToPlay() {
        TimeToPlay -= SecondsPerToast;
    }
}
