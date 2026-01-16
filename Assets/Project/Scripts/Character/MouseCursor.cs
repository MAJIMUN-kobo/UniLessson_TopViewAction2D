using JetBrains.Annotations;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [Header("** Cursor Settings **")]
    public bool isVisibled = false;
    public Vector3 mousePosition = new Vector3(0, 0, 0);

    void Start()
    {
        DisableCursor();
    }

    void Update()
    {
        ChaseMousePinter();
    }

    /// <summary>
    /// マウスポインターを追従するメソッド
    /// </summary>
    public void ChaseMousePinter()
    {
        mousePosition = Input.mousePosition;
        
        Vector3 spritePosition = Camera.main.ScreenToWorldPoint( mousePosition );
        spritePosition.z = 0;

        transform.position = spritePosition;
    }

    /// <summary>
    /// マウスカーソルを有効化するメソッド
    /// </summary>
    public void EnableCursor()
    {
        isVisibled = true;
        Cursor.visible = isVisibled;
    }

    /// <summary>
    /// マウスカーソルを無効化するメソッド
    /// </summary>
    public void DisableCursor()
    {
        isVisibled = false;
        Cursor.visible = isVisibled;
    }
}
