using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI TopScore;
    public GameObject GameOverText;
    
    //private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private bool paused;

    public static PersistentData pData = PersistentData.Instance;
    public static MainManager ManInstance;

    // Start is called before the first frame update
    void Start()
    {
        // start of new code
        if (ManInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        ManInstance = this;
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
        if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                m_GameOver = false;
                //Play();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("TitleScreen");
                m_GameOver = false;
                //Play();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                pData.ClearTopScore();
                m_GameOver = false;
                //Play();
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (paused)
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

    public void AddPoints(int points)
    {
        m_Points += points;
        ScoreText.text = $"Score : {m_Points}";
        PersistentData.Instance.ScoreUpdate(m_Points);
        UpdateTopScore();
    }

    public void UpdateTopScore()
    {
        UnityEngine.Debug.Log("UpdateTopScore Called");
        TopScore.text = $"Top Score: {pData.GetTopName()} {pData.GetTopPoints()}";
    }

    public void GameOver()
    {
        //Pause();
        m_GameOver = true;
        GameOverText.SetActive(true);
        pData.SaveTopScore();
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
    }

    public void Play()
    {
        paused = false;
        Time.timeScale = 1f;
    }

}
