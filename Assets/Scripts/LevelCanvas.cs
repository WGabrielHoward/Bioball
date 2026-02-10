
using System.Net.NetworkInformation;
using UnityEngine;
using Scripts.Systems;

//[DefaultExecutionOrder(500)]
public class LevelCanvas : MonoBehaviour
{

    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI HealthText;

    public TMPro.TextMeshProUGUI TotalScoreText;
    public TMPro.TextMeshProUGUI TopScore;
    
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    public GameObject PauseScreen;

    public static LevelManager pMan;
    public static PersistentData pData;
    public static LevelCanvas Instance;

    //Start is called before the first frame update
    void Start()
    {
        pMan = LevelManager.Instance;
        pData = PersistentData.Instance;
        if (Instance == null)
        {
            Instance = this;
        }
        PlayingSetup();
        TopScoreUpdate(pData.GetTopName(), pData.GetTopPoints());
    }

    
    private void OnEnable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnScoreChanged += ScoreUpdate;
            LevelManager.Instance.OnScoreChanged += TotalScoreUpdate;
        }
        if (PersistentData.Instance != null)
        {
            PersistentData.Instance.TopScoreChanged += TopScoreUpdate;
        }
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnScoreChanged -= ScoreUpdate;
            LevelManager.Instance.OnScoreChanged -= TotalScoreUpdate;
        }
        if (PersistentData.Instance != null)
        {
            PersistentData.Instance.TopScoreChanged -= TopScoreUpdate;
        }
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged -= OnGameStateChanged;
        }
    }

    // Now we only call the screen setActives when state is changed
    private void OnGameStateChanged(GameState from, GameState newState)
    {
        switch (newState)
        {
            case GameState.Defeat:
                GameOverScreen.SetActive(true);
                TotalScoreText.gameObject.SetActive(true);
                break;
            case GameState.Playing:
                PlayingSetup();
                break;
            case GameState.Victory:
                VictoryScreen.SetActive(true);
                NotPlayingSetup();
                break;
            case GameState.Pause:
                PauseScreen.SetActive(true);
                NotPlayingSetup();
                break;
        }
    }

    private void PlayingSetup()
    {
        GameOverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        PauseScreen.SetActive(false);
        TotalScoreText.gameObject.SetActive(false);
        TopScore.gameObject.SetActive(false);

        ScoreText.gameObject.SetActive(true);
        HealthText.gameObject.SetActive(true);
    }

    private void NotPlayingSetup()
    {
        TotalScoreText.gameObject.SetActive(true);
        TopScore.gameObject.SetActive(true);
    }

    public void ScoreUpdate(int currentLevelScore)
    {
        UnityEngine.Debug.Log($"LevelCanvas: ScoreUpdate({currentLevelScore})");
        ScoreText.text = $"Score : {currentLevelScore}";
    }

    public void TotalScoreUpdate(int levelScore)
    {
        UnityEngine.Debug.Log($"LevelCanvas: TotalScoreUpdate({levelScore})");
        TotalScoreText.text = $"Total Score: {pData.GetTotalScore()+levelScore}";
    }

    public void TopScoreUpdate(string topName, int topScore)
    {
        //pMan.Dump();
        UnityEngine.Debug.Log($"LevelCanvas: TopScoreUpdate({topName}, {topScore})");
        TopScore.text =  ($"Top Score: {topName} {topScore}");
    }

    public void HealthUpdate(int health)
    {
        HealthText.text = $"Health: {health}";
    }
}
