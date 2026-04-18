using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("** Manager Settings **")]
    public ScoreManager scoreManager;

    [Header("** uGUI Settings **")]
    public GameObject resultElement;
    public Button retryButton;
    public Button quitButton;

    [HideInInspector]
    public Player player;

    void Start()
    {
        
    }

    void Update()
    {
        SearchPlayer();
        ResultCheckUpdate();
    }

    /// <summary>
    /// Playerのスクリプトを持つオブジェクトを検索するメソッド
    /// </summary>
    public void SearchPlayer()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void ResultCheckUpdate()
    {
        if (player == null) return;

        if(resultElement.activeSelf == false && player.isAlive == false)
        {
            scoreManager.SaveScore();
        }

        if(player.isAlive == false)
        {
            resultElement.SetActive(true);
        }
        else
        {
            resultElement.SetActive(false);
        }
    }
}
