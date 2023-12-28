using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    public ItemData.ItemType itemDate;
    public float value;

    public void Init(ItemData data)
    {
        name = "Passive" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        itemDate = data.itemType;
        switch (itemDate)
        {
            case ItemData.ItemType.Damage:
                value = data.upgradeDamages[0];
                break;
            case ItemData.ItemType.Range:
                value = data.upgradeRange[0];
                break;
            case ItemData.ItemType.CoolTime:
                value = data.upgradeCT[0];
                break;
            case ItemData.ItemType.MoveSpeed:
                value = data.upgradeMoveSpeed[0];
                break;
        }
        ApplyPassive();
    }

    public void LevelUp(float value)
    {
        this.value = value;
        ApplyPassive();
    }

    void CoolDown()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            weapon.coolTime = weapon.baseCoolTime * (1 - value);
        }
    }

    void MoveUp()
    {
        GameManager.instance.player.speed = GameManager.instance.player.baseSpeed * (1 + value);
    }

    void DamageUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
           weapon.damage = weapon.baseDamage * (1 + value);
        }
    }

    void RangeUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            float basevalue = 1 + value;
            weapon.range = weapon.baseRange * (1 + value);
        }
    }

    void ApplyPassive()
    {
        switch (itemDate)
        {
            case ItemData.ItemType.Damage:
                DamageUp();
                break;
            case ItemData.ItemType.Range:
                RangeUp();
                break;
            case ItemData.ItemType.CoolTime:
                CoolDown();
                break;
            case ItemData.ItemType.MoveSpeed:
                MoveUp();
                break;
        }
    }
}
