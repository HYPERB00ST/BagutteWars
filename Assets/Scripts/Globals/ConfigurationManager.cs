using UnityEngine;

[System.Serializable]
public class GameSettings
{
    public float musicVolume;
    public float sfxVolume;
}

public class ConfigurationManager : MonoBehaviour
{
    public static GameSettings gameSettings;

    void Awake()
    {
        if (gameSettings == null)
        {
            gameSettings = new GameSettings();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
