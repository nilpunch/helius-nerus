using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class UpgdageObject : MonoBehaviour
{
    [SerializeField] private UpgradeTypes _type;
    // А эта вещб то походу на свитчкейсах. Создавать да можно по енуму а вот уже применять надо будет свитчами жеж. Или полиморфизм? Типа "ApplyApgrade(Player player)"

    // Откуда-то взять надо.
    private Sprite _icon;


}

public enum UpgradeTypes
{
    Damage,
    Speed,
    Angle,
    Bullets
}
