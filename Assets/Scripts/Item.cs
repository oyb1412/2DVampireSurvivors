using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public int level;
    public Weapon weapon;
    public PassiveItem passive;
    Image iconImage;
    Text nameText;
    Text descText;
    Text levelText;
    public GameObject[] iconObj;
    private void Awake()
    {
        iconImage = GetComponentsInChildren<Image>()[1];
        Text[] texts = GetComponentsInChildren<Text>();


        levelText = texts[0];
        nameText = texts[1];
        descText = texts[2];
        nameText.text = itemData.itemName;
        descText.text = itemData.itemDesc;

        iconImage.sprite = itemData.itemIcon;
        

    }

    private void OnEnable()
    {
        levelText.text = "Lv." + (level + 1);

        switch(itemData.itemType)
        {
            case ItemData.ItemType.Melee:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] * 100, itemData.upgradeRange[level] * 100);
                break;
            case ItemData.ItemType.LongRange:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] * 100);
                break;
            case ItemData.ItemType.Damage:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] * 100);
                break;
            case ItemData.ItemType.Range:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeRange[level] * 100);
                break;
            case ItemData.ItemType.CoolTime:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeCT[level] * 100);
                break;
            case ItemData.ItemType.MoveSpeed:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeMoveSpeed[level] * 100);
                break;
            case ItemData.ItemType.RotateWeapon:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] * 100, itemData.upgradeRange[level] * 100);
                break;
            case ItemData.ItemType.BounceWeapon:
                descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] * 100, itemData.upgradeRange[level] * 100);
                break;
        }
    }



    public void OnClick()
    {
        switch (itemData.itemType)
        {
                  

            case ItemData.ItemType.Melee:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.range;

                    nextDamage = weapon.damage + (5* itemData.upgradeDamages[level]);
                    nextRange = weapon.range + (1 * itemData.upgradeRange[level]);

                    weapon.LevelUp(nextDamage, nextRange);
                }
                iconObj[0].SetActive(true);
                Text obj;
                obj = iconObj[0].GetComponentInChildren<Text>();
                obj.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.LongRange:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else
                {
                    float nextDamage = weapon.damage;

                    nextDamage = weapon.damage +(3* itemData.upgradeDamages[level]);

                    weapon.LevelUp(nextDamage, 1);
                }
                iconObj[1].SetActive(true);
                Text objDagger = iconObj[1].GetComponentInChildren<Text>();
                objDagger.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.Damage:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    passive = newWeapon.AddComponent<PassiveItem>();
                    passive.Init(itemData);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeDamages[level];

                    passive.LevelUp(nextPassiveValue);
                }
                iconObj[4].SetActive(true);
                Text objDamage = iconObj[4].GetComponentInChildren<Text>();
                objDamage.text = "Lv" + (level + 1);
                break;

            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    passive = newWeapon.AddComponent<PassiveItem>();
                    passive.Init(itemData);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeRange[level];

                    passive.LevelUp(nextPassiveValue);
                }
                iconObj[5].SetActive(true);
                Text objRange = iconObj[5].GetComponentInChildren<Text>();
                objRange.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.CoolTime:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    passive = newWeapon.AddComponent<PassiveItem>();
                    passive.Init(itemData);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeCT[level];

                    passive.LevelUp(nextPassiveValue);
                }
                iconObj[6].SetActive(true);
                Text objCT = iconObj[6].GetComponentInChildren<Text>();
                objCT.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.MoveSpeed:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    passive = newWeapon.AddComponent<PassiveItem>();
                    passive.Init(itemData);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeMoveSpeed[level];

                    passive.LevelUp(nextPassiveValue);
                }
                iconObj[7].SetActive(true);
                Text objMove = iconObj[7].GetComponentInChildren<Text>();
                objMove.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.RotateWeapon:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);

                }
                else
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.range;

                    nextDamage = weapon.damage + (5 * itemData.upgradeDamages[level]);
                    nextRange = weapon.range + (1 * itemData.upgradeRange[level]);

                    weapon.LevelUp(nextDamage, nextRange);
                }
                iconObj[2].SetActive(true);
                Text objRotate = iconObj[2].GetComponentInChildren<Text>();
                objRotate.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.BounceWeapon:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);

                }
                else
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.range;

                    nextDamage = weapon.damage + (4 * itemData.upgradeDamages[level]);
                    nextRange = weapon.range + (1 * itemData.upgradeRange[level]);

                    weapon.LevelUp(nextDamage, nextRange);
                }
                iconObj[3].SetActive(true);
                Text objCross = iconObj[3].GetComponentInChildren<Text>();
                objCross.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.hp = GameManager.instance.maxHp;
                break;


        }

        level++;
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Select);
        if (level == itemData.upgradeRange.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
