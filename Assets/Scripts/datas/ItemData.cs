using UnityEngine;

/// <summary>
/// 모든 아이템 데이터를 관리
/// </summary>
[CreateAssetMenu(fileName ="Item", menuName ="Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee,
        LongRange,
        Damage,
        Range,
        Count,
        Heal,
        CoolTime,
        MoveSpeed,
        RotateWeapon,
        BounceWeapon
    }

    [Header("Main Info")]
    public ItemType itemType;
    public int itemId;

    [TextArea]
    public Sprite itemIcon;

    [TextArea]
    public string itemDesc; // 아이템의 데이터 수치 정보

    [TextArea]
    public string itemBaseDesc; // 아이템의 정보
    public int count; // 관통력

    [Header("Option Info")]
    public float damage;
    public int range;
    public float CT; // 쿨타임

    // 아이템 업그레이드시 증가량
    public float[] upgradeCT;
    public float[] upgradeDamages;
    public float[] upgradeRange;
    public float[] upgradeMoveSpeed;

    [Header("Object Info")]
    public GameObject weaponObject; // 아이템 프리펩
}
