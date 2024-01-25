using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private const float BASE_PLAY_TIME = 60f;
    [SerializeField] private const float TIME_PER_TOAST = 6f;
    private const int MAX_LEVEL = 11;
    private const int TIME_TO_LEVEL = 10;

    // Static stats
    internal static float PlayTime;
    internal static int Points {get; private set;} = 0;
    internal static float TotalTimePassed {get; private set;} = 0f;
    internal static int Level {get; private set;} = 1;

    // Flag for scene change
    internal bool inPlay = true;

    void Start() {
        PlayTime = BASE_PLAY_TIME;
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
        PlayTime -= Time.deltaTime;
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
        Points = 0;
    }

    private void LevelUpdate()
    {
        if (Level >= MAX_LEVEL) {
            Level = MAX_LEVEL;
            return;
        }
        Level = (int)TotalTimePassed / TIME_TO_LEVEL;
        if (Level < 1) Level = 1;
    }

    internal void AddPoint() {
        Points += 10;
        ExtendTimeToPlay();
    }

    private void ExtendTimeToPlay()
    {
        PlayTime += TIME_PER_TOAST;
    }

    internal void ShortenTimeToPlay() {
        PlayTime -= TIME_PER_TOAST * 2;
    }
}
