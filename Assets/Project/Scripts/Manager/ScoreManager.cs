using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("** Score Settings **")]
    public int score = 0;
    public int highScore = 0;
    public int scoreCountMax = 99999;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        LoadScore();
        ScoreTextUpdate();
    }

    void Update()
    {
        ScoreTextUpdate();
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
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
