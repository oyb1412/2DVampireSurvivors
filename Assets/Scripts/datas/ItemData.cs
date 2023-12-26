using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public string itemDesc;
    public string itemName;
    public Sprite itemIcon;
    public int count;

    [Header("Option Info")]
    public float damage;
    public int range;
    public float CT;
    public float[] upgradeCT;
    public float[] upgradeDamages;
    public float[] upgradeRange;
    public float[] upgradeMoveSpeed;

    [Header("Object Info")]
    public GameObject weaponObject;
}
