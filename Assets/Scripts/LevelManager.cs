
using Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private int currentLevelPoints;

    public static PersistentData pData;
    public static LevelManager levelMan;

    private int buildIndex;
    private int nextSceneIndex;

    public event System.Action<int> OnScoreChanged;

   
    // Start is called before the first frame update
    void Start()
    {
        pData = PersistentData.Instance;
        levelMan = this;
        GameStateSystem.Instance.TriggerPlay();

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
        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.OnScoreAdded += AddPoints;
        }
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void OnDisable()
    {
        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.OnScoreAdded -= AddPoints;
        }
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged -= OnGameStateChanged;
        }
    }

    public void AddPoints(int points)
    {
        UnityEngine.Debug.Log($"LevMan: AddPoints {points}");
        currentLevelPoints += points;
        OnScoreChanged?.Invoke(currentLevelPoints);
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
