using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("** Effect Value **")]
    public float effectValue = 0;

    /// <summary>
    /// アイテム効果メソッド
    /// </summary>
    /// <param name="player">効果対象</param>
    public void OnActiveEffect(Player player)
    {
        player.OnHeal(effectValue);

        Destroy(gameObject);
    }
}
