
using Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private int currentLevelPoints;
    //private GameState lastState;

    public static PersistentData pData;
    public static LevelManager Instance;

    private LevelCanvas levelCanvas;
    private int buildIndex;
    private int nextSceneIndex;

    public event System.Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        currentLevelPoints = 0;

        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        pData = PersistentData.Instance;
        GameStateSystem.Instance.TriggerPlay();

        levelCanvas = FindAnyObjectByType<LevelCanvas>();

        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;

        AddPoints(0); // sends signal to update canvas
    }

    private void Update()
    {
        HandleStateInput(GameStateSystem.Instance.CurrentState);

    }

    private void OnEnable()
    {
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void OnDisable()
    {
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged -= OnGameStateChanged;
        }
    }

    public void AddPoints(int points)
    {
        // records
        currentLevelPoints += points;
        //OnScoreChanged?.Invoke(currentLevelPoints);
        levelCanvas.ScoreUpdate(currentLevelPoints);
        levelCanvas.TotalScoreUpdate(currentLevelPoints);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen"); // 0, should be TitleScreen
        GameStateSystem.Instance.TriggerPlay();
    }

    public void RestartLevel()
    {
        AddPoints(-currentLevelPoints);
        SceneManager.LoadScene(buildIndex); // Restart this level
        GameStateSystem.Instance.TriggerPlay();
    }

    public void NextLevel()
    {
        pData.AddToTotalScore(currentLevelPoints);
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Next Level
        }
        else
        {
            SceneManager.LoadScene(0);  // 0, should be TitleScreen
        }

        GameStateSystem.Instance.TriggerPlay();
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
