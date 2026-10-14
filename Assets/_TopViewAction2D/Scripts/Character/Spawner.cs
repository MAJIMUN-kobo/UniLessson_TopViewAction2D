using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("** Player Information **")]
    public Player player;

    [Header("** Spawn Settings **")]
    public GameObject spawnPrefab;
    public float spawnInterval = 2.0f;
    public float randomPositionOffsetX = 0.5f;
    public float randomPositionOffsetY = 0.5f;
    public int appearCount = 1;

    [HideInInspector] public float intervalTimer = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        SearchPlayer();
        SpawnUpdate();
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
    /// スポナーを動かすメソッド
    /// </summary>
    public void SpawnUpdate()
    {
        if (player != null && player.isAlive == false) return;

        intervalTimer += Time.deltaTime;
        if (spawnInterval <= intervalTimer)
        {
            Spawn();

            intervalTimer = 0.0f;
        }
    }

    /// <summary>
    /// オブジェクトを生成するメソッド
    /// </summary>
    public void Spawn()
    {
        if(spawnPrefab == null)
        {
            Debug.LogError("Null Error: spawnPrefab");
            return;
        }

        for (int i = 0; i < appearCount; i++)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-randomPositionOffsetX, randomPositionOffsetX);
            spawnPosition.y += Random.Range(-randomPositionOffsetY, randomPositionOffsetY);

            GameObject clone = Instantiate(spawnPrefab, spawnPosition, transform.rotation);
        }
    }
}
