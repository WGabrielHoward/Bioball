using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
   
    public Text ScoreText;
    public Text TopScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public static PersistentData pData = PersistentData.Instance;

    // Start is called before the first frame update
    void Start()
    {

        UpdateTopScore();
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
               
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                pData.ClearTopScore();
            }
        }
    }

    void AddPoints(int points)
    {
        m_Points += points;
        ScoreText.text = $"Score : {m_Points}";
        PersistentData.Instance.ScoreUpdate(m_Points);
        UpdateTopScore();
    }

    public void UpdateTopScore()
    {
        TopScore.text = $"Top Score: {pData.GetTopName()} {pData.GetTopPoints()}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        pData.SaveTopScore();
    }


}
