
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MenuUI : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI TopScore;
    [SerializeField] private TMPro.TMP_InputField playerName;
    [SerializeField] private GameObject hiddenObj;
    private bool hidden;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerName.characterLimit = 12;
        if (PersistentData.Instance != null)
        {
            TopScoreUpdate();
        }
        hiddenObj.SetActive(false);
        hidden = true;
    }

    public void StartPlay()
    {
        if (PersistentData.Instance != null)
        {
            PersistentData.Instance.playerName = playerName.text.ToString();
        }
        SceneManager.LoadScene("Scene_0");
    }

    public void TopScoreUpdate()
    {
        //pMan.Dump();
        TopScore.text = $"Top Score: {PersistentData.Instance.GetTopName()} {PersistentData.Instance.GetTopPoints()}";
    }

    public void ClearMemory()
    {
        PersistentData.Instance.ClearTopScore();
        TopScoreUpdate();
    }

    public void HideOrReveal()
    {
        if (hiddenObj)
        {
            if (hidden)
            {
                hiddenObj.SetActive(hidden);
                hidden = false;
            }
            else
            {
                hiddenObj.SetActive(hidden);
                hidden = true;
            }

        }
    }

   

    public void Exit()
    {
        PersistentData.Instance.SaveTopScore();     // enable this to autoSave any changes, else it will go with whatever was last saved
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
