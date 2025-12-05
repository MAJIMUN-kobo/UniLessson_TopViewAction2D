using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("** Player Information **")]
    public Player player;

    [Header("** uGUI Settings **")]
    public TextMeshProUGUI healthText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SearchPlayer();
        HealthTextUpdate();
    }

    public void SearchPlayer()
    {
        if ( player == null )
        {
            player = FindAnyObjectByType<Player>();
        }
    }

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
