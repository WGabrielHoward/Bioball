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

public class MainManager : MonoBehaviour
{
    private int m_Points;       // Should I move this to the player?
    private static PlayState state;

    public static PersistentData pData;
    public static MainManager ManInstance;

    private LevelCanvas levelCanvas;

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
    }

    private void Update()
    {
        //// Only usefull if you intend to wait till space to start
        //if (!m_Started)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        m_Started = true;
        //    }
            
        //}
        if (GetState()==PlayState.gameOver || GetState()==PlayState.victory)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Play();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("TitleScreen");
                //Play();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                pData.ClearTopScore();
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GetState() == PlayState.paused)
                {
                    Play();
                }
                else
                {
                    Pause();
                }
            }
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
