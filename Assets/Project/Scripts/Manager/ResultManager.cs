using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [Header("** Player Information **")]
    public Player player;

    [Header("** uGUI Settings **")]
    public GameObject resultElement;

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

    public void ResultCheckUpdate()
    {
        if (player == null) return;

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
