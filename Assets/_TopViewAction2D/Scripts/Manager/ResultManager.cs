using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Header("** Manager Settings **")]
    public ScoreManager scoreManager;       // スコアマネージャーの参照を保持する変数

    [Header("** uGUI Settings **")]
    public GameObject resultElement;        // リザルト画面のUI要素を保持する変数

    [HideInInspector]
    public Player player;                   // Playerのスクリプトを持つオブジェクトの参照を保持する変数

    void Start()
    {
        // 特になし
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

    /// <summary>
    /// ゲームをリトライするメソッド
    /// ※ [RetryButton] のクリックイベントに設定しています。
    /// </summary>
    public void GameRetry()
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

    /// <summary>
    /// ゲームの結果をチェックするメソッド
    /// </summary>
    public void ResultCheckUpdate()
    {
        // player が null または resultElement が null の場合は処理を中断する
        if (player == null || resultElement == null) return;

        // player が死亡していて、かつ resultElement が非アクティブの場合にスコアを保存する
        // ※ セーブ直後に resultElement をアクティブにすることで、スコアが二重に保存されることを防いでいます。
        if (resultElement.activeSelf == false && player.isAlive == false)
        {
            scoreManager.SaveScore();
        }

        // player が死亡している場合は resultElement をアクティブにする
        if (player.isAlive == false)
        {
            resultElement.SetActive(true);
        }
        else
        {
            resultElement.SetActive(false);
        }
    }
}
