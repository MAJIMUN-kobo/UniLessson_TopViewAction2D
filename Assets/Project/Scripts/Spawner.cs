using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("** Spawn Settings **")]
    public GameObject spawnPrefab;
    public float spawnInterval = 2.0f;
    public float intervalTimer = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        SpawnUpdate();
    }

    /// <summary>
    /// スポナーを動かすメソッド
    /// </summary>
    public void SpawnUpdate()
    {
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

        GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation);
    }
}
