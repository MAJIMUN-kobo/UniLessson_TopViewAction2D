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

    [Header("** Renderer Settings **")]
    public SpriteRenderer spriteRenderer;

    [Header("** Animation Settings **")]
    public Animator animator;

    [Header("** Audio Settings **")]
    public AudioSource audioSource;

    [Header("** Particle Settings **")]
    public GameObject deathParticlePrefab;

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

            OnDamage( 10.0f );
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
            OnDeath();
        }
    }

    /// <summary>
    /// 死亡時の処理メソッド
    /// </summary>
    public void OnDeath()
    {
        gameObject.SetActive( false );

        AddScore( addScore );

        CreateDeathParticle();
    }

    /// <summary>
    /// 死亡時のパーティクルを生成するメソッド
    /// </summary>
    public void CreateDeathParticle()
    {
        Vector3 particlePosition = transform.position;
        Vector3 particleRotation = new Vector3(-90, 0, 0);
        
        GameObject particle = Instantiate( deathParticlePrefab );
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
