using UnityEngine;

public class TakaraBox : MonoBehaviour
{
    [Header("** Drop Settings **")]
    public GameObject dropItemPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.transform.tag == "Weapon" )
        {
            Instantiate( dropItemPrefab, transform.position, transform.rotation );
            Destroy( gameObject );
        }
    }
}
