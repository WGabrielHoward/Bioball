using UnityEngine;
using System.IO;

public class PersistentData : MonoBehaviour
{
    public string playerName;
    public int playerTotalPoints;
    // public int playerPoints // by level?

    private int topPoints;
    private string topPointsName;
    public event System.Action<string , int > TopScoreChanged;

    public static PersistentData Instance;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadTopScore();
    }

    // This does not include ongoing level
    public int GetTotalScore()
    {
        return playerTotalPoints;
    }

    public string GetTopName()
    {
        string topName = topPointsName;
        return topName;
    }

    public int GetTopPoints()
    {
        int highScore = topPoints;
        return highScore;
    }

    public void AddToTotalScore(int levelScore)
    {
        playerTotalPoints += levelScore;
        TopScoreUpdate();
    }

    public void TopScoreUpdate()
    {
        if (playerTotalPoints > topPoints)
        {
            topPoints = playerTotalPoints;
            topPointsName = playerName;
            SaveTopScore();
            TopScoreChanged?.Invoke(playerName, playerTotalPoints);
                        
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int topPoints;
        public string topPointsName;
    }

    public void SaveTopScore()
    {
        // Making new saveData
        SaveData data = new SaveData();
        data.topPoints = topPoints;
        data.topPointsName = topPointsName;

        // Serializing to json
        string json = JsonUtility.ToJson(data);
        // Saving
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        // For WebGL builds, explicitly sync files with the browser's filesystem
//#if UNITY_WEBGL
        PlayerPrefs.SetString("GameSaveData", json);
        // Important: You must call PlayerPrefs.Save() explicitly in WebGL
        // as OnApplicationQuit is not called by the browser.
        PlayerPrefs.Save();
//#endif
    }

    public void LoadTopScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            topPoints = data.topPoints;
            topPointsName = data.topPointsName;
        }
        // For WebGL builds, explicitly sync files with the browser's filesystem
//#if UNITY_WEBGL
        if (PlayerPrefs.HasKey("GameSaveData"))
        {
            string jsonString = PlayerPrefs.GetString("GameSaveData");
            SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
            topPoints = data.topPoints;
            topPointsName = data.topPointsName;
        }
        
//#endif
    }

    public void ClearTotalScore()
    {
        playerTotalPoints = 0;
    }

    public void ClearTopScore()
    {
        topPoints = 0;
        topPointsName = "";
        SaveTopScore();
    }
}
