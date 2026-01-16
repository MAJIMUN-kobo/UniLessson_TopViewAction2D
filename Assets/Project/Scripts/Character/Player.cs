using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("** Controller Input Info **")]
    public bool upMoveKey = false;
    public bool downMoveKey = false;
    public bool leftMoveKey = false;
    public bool rightMoveKey = false;
    public Vector3 mousePosition = new Vector3(0, 0, 0);

    [Header("** Weapon Settings **")]
    public GameObject handPoint;

    [Header("** Movement Settings **")]
    public float moveSpeed = 5.0f;
    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(0, 0, 0);

    [Header("** Health Settings **")]
    public float healthMax = 100.0f;
    public float health = 100.0f;
    public bool isAlive = true;

    [Header("** Attack Settings **")]
    public float attackValue = 20.0f;

    [Header("** Renderer Settings **")]
    public SpriteRenderer spriteRenderer;

    [Header("** Animation Settings **")]
    public Animator animator;

    [Header("** Audio Settings **")]
    public AudioSource audioSource;

    [Header("** Particle Settings **")]
    public GameObject deathParticlePrefab;
    public GameObject damageParticlePrefab;

    void Start()
    {

    }

    void Update()
    {
        if (isAlive == false) return;

        InputUpdate();
        MoveUpdate();
        HandUpdate();
        DirectionUpdate();
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.transform.tag == "Enemy" )
        {
            NormalEnemy enemy = collision.transform.GetComponent<NormalEnemy>();
            if (enemy == null) return;

            Vector3 posA = transform.position;
            Vector3 posB = enemy.transform.position;
            Vector3 knockBackVector = posB - posA;
            float enemyAtk = enemy.attackValue;

            KnockBack( knockBackVector, 0.5f );
            CreateParticle(damageParticlePrefab);
            OnDamage(enemyAtk);
            
            if( HealthCheck() == false && isAlive == true )
            {
                CreateParticle(deathParticlePrefab);
                OnDeath();
            }
        }

        if( collision.transform.tag == "Item" )
        {
            Item item = collision.transform.GetComponent<Item>();
            if (item == null) return;

            item.OnActiveEffect( this );
        }
    }

    /// <summary>
    /// 入力を更新するメソッド
    /// </summary>
    public void InputUpdate()
    {
        upMoveKey     = Input.GetKey( KeyCode.W );
        downMoveKey   = Input.GetKey( KeyCode.S );
        leftMoveKey   = Input.GetKey( KeyCode.A );
        rightMoveKey  = Input.GetKey( KeyCode.D );
        mousePosition = Input.mousePosition;
    }

    /// <summary>
    /// 座標を更新するメソッド
    /// </summary>
    public void MoveUpdate()
    {
        moveVector = new Vector3(0, 0, 0);

        if ( upMoveKey == true )
        {
            moveVector.y = moveSpeed;
        }

        if ( downMoveKey == true )
        {
            moveVector.y = -moveSpeed;
        }

        if ( leftMoveKey == true )
        {
            moveVector.x = -moveSpeed;
            direction.x = moveVector.x;
        }

        if( rightMoveKey == true )
        {
            moveVector.x = moveSpeed;
            direction.x = moveVector.x;
        }

        Vector3 velocity = moveVector * Time.deltaTime;
        transform.Translate( velocity );
    }

    /// <summary>
    /// 手（武器）の向きを更新するメソッド
    /// </summary>
    public void HandUpdate()
    {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint( mousePosition );

        Vector3 positionDiff = mousePositionWorld - transform.position;

        float radian = Mathf.Atan2( positionDiff.y, positionDiff.x );

        float euler = ( radian / Mathf.PI * 180 ) - 90;

        handPoint.transform.eulerAngles = new Vector3( 0, 0, euler );
    }

    /// <summary>
    /// 体の向きを更新するメソッド
    /// </summary>
    public void DirectionUpdate()
    {
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
    /// 回復時の処理メソッド
    /// </summary>
    /// <param name="heal">回復量</param>
    public void OnHeal( float heal )
    {
        health += heal;

        if( health > healthMax )
        {
            health = healthMax;
        }
    }

    /// <summary>
    /// 被ダメージ時の処理メソッド
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void OnDamage( float damage )
    {
        health -= damage;

        if(health < 0)
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
    /// 生存チェックメソッド
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
    public void CreateParticle( GameObject original )
    {
        if(original == null)
        {
            Debug.LogWarning("パーティクルのプレハブが設定されていません。");
            return;
        }

        Vector3 particlePosition = transform.position;
        Vector3 particleRotation = new Vector3(-90, 0, 0);
        
        GameObject particle = Instantiate( original );
        particle.transform.position = particlePosition;
        particle.transform.eulerAngles = particleRotation;

        Destroy(particle, 1.0f);
    }
}
