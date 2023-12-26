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
        value = 0;
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
            weapon.coolTime = weapon.coolTime - (1 * value);
        }
    }

    void MoveUp()
    {
        GameManager.instance.player.speed = GameManager.instance.player.speed + (1 * value);
    }

    void DamageUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.damage = weapon.damage + (5 * value);
                    break;
                case 1:
                    weapon.damage = weapon.damage + (3 * value);
                    break;
            }
        }
    }

    void RangeUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
           weapon.range = weapon.range + (1 * value);
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
