
using Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static PersistentData pData;

    private int buildIndex;
    private int nextSceneIndex;
    private int titleScreenIndex;
       
    // Start is called before the first frame update
    void Start()
    {
        pData = PersistentData.Instance;

        ScoreSystem.Instance.ResetLevelScore();
        GameStateSystem.Instance.TriggerPlay();

        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;
        titleScreenIndex = 0; // SceneManager.GetSceneByName("TitleScreen").buildIndex;   // should be 0 aka main menu
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

    public void LoadLevel(int levelIndex)
    {
        ScoreSystem.Instance.ResetLevelScore();
        SceneManager.LoadScene(levelIndex);     // 0 = main menu, buildIndex = this level, nextSceneIndex = next level
        GameStateSystem.Instance.TriggerPlay();
    }


    public void NextLevel()
    {
        // adding currentLevelScore to total
        pData.AddToTotalScore(ScoreSystem.Instance.GetCurrentLevelScore());
        
        if(nextSceneIndex>= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = titleScreenIndex;
        }

        LoadLevel(nextSceneIndex);
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
            GameStateSystem.Instance.TriggerPlay(); // play
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
    }

    void HandleGameOverInput()
    {        
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
    }

    void HandleVictoryInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();                            // go to next level
        }
    }


}
