using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("** Timer Settings **")]
    public float timeLimit = 10.0f;
    public float gameTimer = 0.0f;

    [Header("** uGUI Settings **")]
    public TextMeshProUGUI timerText;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        TimerUpdate();
        TimerTextUpdate();
    }

    public void Initialize()
    {
        gameTimer = timeLimit;
    }

    public void TimerUpdate()
    {
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            gameTimer = 0;
        }
    }

    public void TimerTextUpdate()
    {
        if (timerText == null)
        {
            Debug.LogError("Null Error: timeText");
            return;
        }

        timerText.text = gameTimer.ToString("F2");
    }
}
