using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("** Player Information **")]
    public Player player;

    [Header("** uGUI Settings **")]
    public TextMeshProUGUI healthText;

    void Update()
    {
        SearchPlayer();
        HealthTextUpdate();
    }

    /// <summary>
    /// Playerのスクリプトを持つオブジェクトを検索するメソッド
    /// </summary>
    public void SearchPlayer()
    {
        if ( player == null )
        {
            player = FindAnyObjectByType<Player>();
        }
    }

    /// <summary>
    /// 体力のテキストUIを更新するメソッド
    /// </summary>
    public void HealthTextUpdate()
    {
        if(healthText == null)
        {
            Debug.LogError("Null Error: healthText");
            return;
        }

        if(player == null)
        {
            Debug.LogError("Null Error: player");
            return;
        }

        if(player.health <= player.healthMax * 0.1f)
        {
            healthText.color = Color.red;
        }
        else if(player.health <= player.healthMax * 0.5f)
        {
            healthText.color = Color.yellow;
        }

        healthText.text = player.health.ToString("F0");
    }
}
