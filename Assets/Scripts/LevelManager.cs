
using Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private int m_Points;
    //private GameState lastState;

    public static PersistentData pData;
    public static LevelManager ManInstance;

    private LevelCanvas levelCanvas;
    private int buildIndex;
    private int nextSceneIndex;

    private void Awake()
    {
        if (ManInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Points = 0;

        ManInstance = this;
        pData = PersistentData.Instance;
        //lastState = GameState.Undefined;
}

    // Start is called before the first frame update
    void Start()
    {
        m_Points = 0;
        GameStateSystem.Instance.TriggerPlay();

        levelCanvas = FindAnyObjectByType<LevelCanvas>();

        UpdateTotalScore();
        UpdateTopScore();
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;
    }

    private void Update()
    {
        HandleStateInput(GameStateSystem.Instance.CurrentState);

    }

    private void OnEnable()
    {
        GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameStateSystem.Instance != null)
            GameStateSystem.Instance.OnStateChanged -= OnGameStateChanged;
    }


    public void TriggerPlay()
    {
        GameStateSystem.Instance.TriggerPlay();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen"); // 0, should be TitleScreen
        pData.TopScoreUpdate();
        GameStateSystem.Instance.TriggerPlay();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(buildIndex); // Restart this level
        GameStateSystem.Instance.TriggerPlay();
    }

    public void NextLevel()
    {
        AddLevelToTotal();
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Next Level
            pData.TopScoreUpdate();
        }
        else
        {
            SceneManager.LoadScene(0);  // 0, should be TitleScreen
            pData.TopScoreUpdate();
        }
        GameStateSystem.Instance.TriggerPlay();
    }

    
    public void AddPoints(int points)
    {
        m_Points += points;
        UpdateLevelScore();
    }

    public void AddLevelToTotal()
    {
        pData.AddToTotalScore(m_Points);
        UpdateTopScore();
    }
    
    public int GetScore()
    {
        return m_Points;
    }

    public int GetTotalScore()
    {
        // returns sum of total score and current level score
        return pData.GetTotalScore() + m_Points;
    }

    public void UpdateLevelScore()
    {
        levelCanvas.ScoreUpdate();
        UpdateTotalScore();
    }

    public void UpdateTotalScore()
    {
        levelCanvas.TotalScoreUpdate();
    }

    public void UpdateTopScore()
    {
        levelCanvas.TopScoreUpdate();
    }

    public string GetTopScoreText()
    {
        string topScoreText = $"Top Score: {pData.GetTopName()} {pData.GetTopPoints()}";
        return topScoreText;
    }


    public void Dump()
    {
        UnityEngine.Debug.Log("mainManager Exists!");

    }

    void OnGameStateChanged(GameState from, GameState to)
    {
        switch (to)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Pause:
                Time.timeScale = 0f;
                break;

            case GameState.Defeat:
                Time.timeScale = 0f;
                pData.SaveTopScore();
                break;

            case GameState.Victory:
                Time.timeScale = 0f;
                pData.SaveTopScore();
                break;
        }
    }
  

    void HandleStateInput(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.P))
                    GameStateSystem.Instance.TriggerPause();
                break;

            case GameState.Pause:
                HandlePauseInput();
                break;
            case GameState.Defeat:
                HandleGameOverInput();                
                break;
            case GameState.Victory:
                HandleVictoryInput();
                break;
        }
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStateSystem.Instance.TriggerPlay();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
    }

    void HandleGameOverInput()
    {        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
    }
    void HandleVictoryInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
    }


}
