
using UnityEngine;

//[DefaultExecutionOrder(500)]
public class LevelCanvas : MonoBehaviour
{

    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI TopScore;
    public TMPro.TextMeshProUGUI HealthText;
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;

    private LevelManager pMan;
    //Start is called before the first frame update
    void Start()
    {
        GameOverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        pMan = LevelManager.ManInstance;
    }

    // change the game over update to a listerner or event (check that it is actually better)
    void LateUpdate()
    {
        PlayState tmpState = pMan.GetState();
        switch (tmpState)
        {
            case PlayState.gameOver:
                GameOverScreen.SetActive(true);
                break;
            case PlayState.playing:     
                GameOverScreen.SetActive(false);  // I'm not a fan of calling this so often.
                break;
            case PlayState.victory:
                VictoryScreen.SetActive(true);
                break;
            // Room for other cases
        }
    }

    public void ScoreUpdate()
    {
        ScoreText.text = $"Score : {pMan.GetScore()}";
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
