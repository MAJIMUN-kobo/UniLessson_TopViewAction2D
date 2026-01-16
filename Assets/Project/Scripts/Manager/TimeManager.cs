using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("** Timer Settings **")]
    public float timeLimit = 999.0f;

    [Header("** uGUI Settings **")]
    public TextMeshProUGUI timerText;

    [HideInInspector] public float gameTimer = 0.0f;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        TimerUpdate();
        TimerTextUpdate();
    }

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    public void Initialize()
    {
        gameTimer = 0;
    }

    /// <summary>
    /// 時間を更新するメソッド
    /// </summary>
    public void TimerUpdate()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer > timeLimit)
        {
            gameTimer = timeLimit;
        }
    }

    /// <summary>
    /// 時間のテキストを更新するメソッド
    /// </summary>
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
