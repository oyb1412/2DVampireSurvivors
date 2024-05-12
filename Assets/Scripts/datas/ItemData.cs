using UnityEngine;

/// <summary>
/// ��� ������ �����͸� ����
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
    public string itemDesc; // �������� ������ ��ġ ����

    [TextArea]
    public string itemBaseDesc; // �������� ����
    public int count; // �����

    [Header("Option Info")]
    public float damage;
    public int range;
    public float CT; // ��Ÿ��

    // ������ ���׷��̵�� ������
    public float[] upgradeCT;
    public float[] upgradeDamages;
    public float[] upgradeRange;
    public float[] upgradeMoveSpeed;

    [Header("Object Info")]
    public GameObject weaponObject; // ������ ������
}
