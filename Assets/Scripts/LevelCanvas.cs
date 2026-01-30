
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

    private LevelManager pMan;

    //Start is called before the first frame update
    void Start()
    {
        pMan = LevelManager.ManInstance;
        PlayingSetup();
    }

    // change the game over update to a listerner or event (check that it is actually better)
    void Update()
    {
        
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

    // Screen actives called outside of this
    // only used for paused and victory right now
    private void NotPlayingSetup()
    {
        TotalScoreText.gameObject.SetActive(true);
        TopScore.gameObject.SetActive(true);
    }

    public void ScoreUpdate()
    {
        ScoreText.text = $"Score : {pMan.GetScore()}";
    }

    public void TotalScoreUpdate()
    {
        // pMan.GetTotalScore() returns sum of all prior and current level points
        TotalScoreText.text = $"Total Score: {pMan.GetTotalScore()}";
    }

    public void TopScoreUpdate()
    {
        //pMan.Dump();
        TopScore.text = pMan.GetTopScoreText();
    }

    public void HealthUpdate(int health)
    {
        HealthText.text = $"Health: {health}";
    }
}
