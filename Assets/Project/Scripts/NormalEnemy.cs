using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [Header("** Movement Settings **")]
    public float moveSpeed = 5.0f;
    public GameObject chaseTarget;
    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(0, 0, 0);

    [Header("** Health Settings **")]
    public float healthMax = 10.0f;
    public float health = 10.0f;
    public bool isAlive = true;

    [Header("** Renderer Settings **")]
    public SpriteRenderer spriteRenderer;

    [Header("** Animation Settings **")]
    public Animator animator;

    [Header("** Audio Settings **")]
    public AudioSource audioSource;

    [Header("** Particle Settings **")]
    public GameObject deathParticlePrefab;
    public GameObject damageParticlePrefab;

    [Header("** Score Settings **")]
    public ScoreManager scoreManager;
    public int addScore = 10;

    void Start()
    {
        SearchPlayer();
        SearchScoreManager();
    }

    void Update()
    {
        if( isAlive == false )
        {
            return;
        }

        MoveUpdate();
        DirectionUpdate();
    }

    void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.transform.tag == "Weapon" )
        {
            Vector3 posA = transform.position;
            Vector3 posB = collision.transform.position;
            Vector3 knockBackVector = posB - posA;

            KnockBack( knockBackVector, 0.5f );
            CreateParticle( damageParticlePrefab );

            OnDamage( 10.0f );

            if (HealthCheck() == false && isAlive == true)
            {
                AddScore(addScore);
                CreateParticle(deathParticlePrefab);
                OnDeath();
            }
        }
    }

    /// <summary>
    /// 追跡ターゲットを探索するメソッド
    /// </summary>
    public void SearchPlayer()
    {
        chaseTarget = GameObject.Find("Player");
    }

    /// <summary>
    /// ScoreManagerを探索するメソッド
    /// </summary>
    public void SearchScoreManager()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    /// <summary>
    /// 座標を更新するメソッド
    /// </summary>
    public void MoveUpdate()
    {
        if(chaseTarget == null)
        {
            Debug.LogError("Null Error: chaseTarget");
            return;
        }

        moveVector = new Vector3(0, 0, 0);

        Vector3 targetDiff = chaseTarget.transform.position - transform.position;
        moveVector = targetDiff.normalized * moveSpeed;

        Vector3 velocity = moveVector * Time.deltaTime;
        direction = velocity;
        transform.Translate( velocity );
    }

    /// <summary>
    /// 体の向きを更新するメソッド
    /// </summary>
    public void DirectionUpdate()
    {
        float moveX = moveVector.x;

        spriteRenderer.flipX = true;

        if(direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

        /// <summary>
    /// ノックバック処理メソッド
    /// </summary>
    /// <param name="power">ノックバックの強さ</param>
    public void KnockBack( Vector3 vector, float power )
    {
        transform.position += -vector * power;
    }

        /// <summary>
    /// 被ダメージ時の処理メソッド
    /// </summary>
    public void OnDamage( float damage )
    {
        health -= damage;

        if( health <= 0 )
        {
            health = 0;
        }
    }

    /// <summary>
    /// 死亡時の処理メソッド
    /// </summary>
    public void OnDeath()
    {
        isAlive = false;
        gameObject.SetActive( false );
    }

    /// <summary>
    /// 体力チェックメソッド
    /// </summary>
    /// <returns>生存フラグ</returns>
    public bool HealthCheck()
    {
        if (health <= 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 死亡時のパーティクルを生成するメソッド
    /// </summary>
    /// <param name="original">複製オブジェクト</param>
    public void CreateParticle(GameObject original)
    {
        if (original == null)
        {
            Debug.LogWarning("パーティクルのプレハブが設定されていません。");
            return;
        }

        Vector3 particlePosition = transform.position;
        Vector3 particleRotation = new Vector3(-90, 0, 0);

        GameObject particle = Instantiate(original);
        particle.transform.position = particlePosition;
        particle.transform.eulerAngles = particleRotation;

        Destroy(particle, 1.0f);
    }

    /// <summary>
    /// スコアを加算するメソッド
    /// </summary>
    /// <param name="add">加算値</param>
    public void AddScore(int add)
    {
        scoreManager.AddScore( add );
    }
}
