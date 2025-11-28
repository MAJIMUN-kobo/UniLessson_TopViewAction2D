using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("** Keyboard Input Info **")]
    public bool upMoveKey = false;
    public bool downMoveKey = false;
    public bool leftMoveKey = false;
    public bool rightMoveKey = false;

    [Header("** Movement Info **")]
    public float moveSpeed = 5.0f;
    public Vector3 moveVector = new Vector3(0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        moveVector = new Vector3(0, 0, 0);

        upMoveKey = Input.GetKey(KeyCode.W);
        downMoveKey = Input.GetKey(KeyCode.S);
        leftMoveKey = Input.GetKey(KeyCode.A);
        rightMoveKey = Input.GetKey(KeyCode.D);

        if (upMoveKey == true)
        {
            moveVector = new Vector3(0, moveSpeed, 0);
        }

        if (downMoveKey == true)
        {
            moveVector = new Vector3(0, -moveSpeed, 0);
        }

        if (leftMoveKey == true)
        {
            moveVector = new Vector3(-moveSpeed, 0, 0);
        }

        if(rightMoveKey == true)
        {
            moveVector = new Vector3(moveSpeed, 0, 0);

        }

        transform.Translate( moveVector * Time.deltaTime );
    }
}
