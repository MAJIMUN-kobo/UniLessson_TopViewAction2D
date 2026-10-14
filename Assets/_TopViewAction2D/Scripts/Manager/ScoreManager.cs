using TMPro;
using UnityEngine;
using unityroom.Api;

public class ScoreManager : MonoBehaviour
{
    [Header("** Score Settings **")]
    public int score = 0;
    public int highScore = 0;
    public int scoreCountMax = 99999;

    [Header("** uGUI Settings **")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI resultHighScoreText;

    void Start()
    {
        LoadScore();
        ScoreTextUpdate();
    }

    void Update()
    {
        ScoreTextUpdate();
        ResultScoreUpdate();
        ResultHighScoreUpdate();
    }

    public void AddScore( int add )
    {
        score += add;

        if(scoreCountMax <= score)
        {
            score = scoreCountMax;
        }

        if(highScore < score)
        {
            highScore = score;
        }
    }

    public void ScoreTextUpdate()
    {
        if(scoreText == null)
        {
            Debug.LogError("Null Error: scoreText");
            return;
        }

        scoreText.text = score.ToString("D5");
        resultHighScoreText.text = highScore.ToString("D5");
    }

    public void ResultScoreUpdate()
    {
        if(resultScoreText == null)
        {
            Debug.LogError("Null Error: resultScoreText");
            return;
        }

        resultScoreText.text = score.ToString("D5");
    }

    public void ResultHighScoreUpdate()
    {
        if(resultHighScoreText == null)
        {
            Debug.LogError("Null Error: resultHighScoreText");
            return;
        }

        resultHighScoreText.text = highScore.ToString("D5");
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

#if UNITY_WEBGL
        // Unityroom 用のスコア保存処理
        UnityroomApiClient.Instance.SendScore(1, highScore, ScoreboardWriteMode.HighScoreDesc);
#endif

    }

    public void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
