using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("** uGUI Settings **")]
    public GameObject titleElement;         // タイトル画面のUI要素を保持する変数
    public TextMeshProUGUI highScoreText;   // ハイスコアを表示するテキストの参照を保持する変数

    void Start()
    {
        LoadHighScore();
    }

    void Update()
    {
        // 特になし
    }

    public void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString("D5");
    }

    /// <summary>
    /// ゲームを開始するメソッド
    /// ※ [StartButton] のクリックイベントに設定しています。
    /// </summary>
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// ゲームを終了するメソッド
    /// ※ [QuitButton] のクリックイベントに設定しています。
    /// </summary>
    public void GameQuit()
    {
        Application.Quit();
    }
}
