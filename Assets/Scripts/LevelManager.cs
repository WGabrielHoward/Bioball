using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayState
{
    waiting,
    playing,
    paused,
    gameOver,
    victory,
    undefined
}

public class LevelManager : MonoBehaviour
{
    private int m_Points;       // Should I move this to the player?
    private static PlayState state;

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
        state = PlayState.waiting;
        pData = PersistentData.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Points = 0;
        state = PlayState.playing;

        levelCanvas = FindAnyObjectByType<LevelCanvas>();
        
        UpdateTopScore();
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;
    }

    private void Update()
    {
        switch (state)
        {
            case PlayState.gameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(buildIndex); // Restart this level
                    Play();
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    SceneManager.LoadScene("TitleScreen"); // 0, should be TitleScreen
                    Play();
                }
                break;
            case PlayState.victory:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(nextSceneIndex); // Next Level
                    }
                    else
                    {
                        SceneManager.LoadScene(0);  // 0, should be TitleScreen
                    }
                    Play();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(buildIndex); // Restart this level
                    Play();
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    SceneManager.LoadScene("TitleScreen"); // 0, should be TitleScreen
                    Play();
                }
                break;
            case PlayState.playing:
                if (Input.GetKeyDown(KeyCode.P))
                { Pause(); }
                break;
            case PlayState.paused:
                if (Input.GetKeyDown(KeyCode.P))
                { Play(); }
                break;
            
        }
        
    }

    private void SetState(PlayState newState)
    {
        state = newState;
    }

    public void AddPoints(int points)
    {
        m_Points += points;
        levelCanvas.ScoreUpdate();
        PersistentData.Instance.ScoreUpdate(m_Points);
        UpdateTopScore();
    }
    
    public int GetScore()
    {
        return m_Points;
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

    public void GameOver()
    {
        //Pause();
        Time.timeScale = 0f;
        SetState(PlayState.gameOver);
        pData.SaveTopScore();
    }

    public void Victory()
    {
        Time.timeScale = 0f;
        SetState(PlayState.victory);
        pData.SaveTopScore();
    }

    public PlayState GetState()
    {
        return state;
    }

    public void Pause()
    {
        SetState(PlayState.paused);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        SetState(PlayState.playing);
        Time.timeScale = 1f;
    }

    public void Dump()
    {
        UnityEngine.Debug.Log("mainManager Exists!");

    }
}
