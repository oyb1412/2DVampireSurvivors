using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ �� ���� ������ ������ ������ ����
/// </summary>
public class Item : MonoBehaviour
{
    #region Variable
    //�������� ������(��ũ���ͺ� ������Ʈ)
    [SerializeField]private ItemData itemData;
    //���� �������� ����
    private int level;
    private Weapon weapon;
    private PassiveItem passive;
    private Image iconImage;
    private Text levelText;
    private Text descText;
    [SerializeField]private GameObject[] iconObj;
    #endregion

    #region InitMethod
    private void Awake()
    {
        iconImage = GetComponentsInChildren<Image>()[1];
        Text[] saveText = GetComponentsInChildren<Text>();
        levelText = saveText[0];
        descText = saveText[1];

        iconImage.sprite = itemData.itemIcon;
    }

    /// <summary>
    /// UIȰ��ȭ��, ������ Ÿ�Կ� �´� ������ itemData���� ������ UI�� ǥ��
    /// </summary>
    private void OnEnable()
    {
        levelText.text = "Lv." + (level + 1);
        if (level == 0)
        {
            descText.text = itemData.itemBaseDesc;
        }
        else if(level > 5)
        {
            switch (itemData.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.RotateWeapon:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] / 10, itemData.upgradeRange[level] * 10);
                    break;
                case ItemData.ItemType.LongRange:
                case ItemData.ItemType.BounceWeapon:
                case ItemData.ItemType.Damage:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level] / 10);
                    break;
                case ItemData.ItemType.Range:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeRange[level] * 10);
                    break;
                case ItemData.ItemType.CoolTime:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeCT[level] * 10);
                    break;
                case ItemData.ItemType.MoveSpeed:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeMoveSpeed[level] * 10);
                    break;
                case ItemData.ItemType.Heal:
                    descText.text = string.Format(itemData.itemDesc);
                    break;


            }
        }
        else
        {
            switch (itemData.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.RotateWeapon:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level], itemData.upgradeRange[level] * 100);
                    break;
                case ItemData.ItemType.LongRange:
                case ItemData.ItemType.BounceWeapon:
                case ItemData.ItemType.Damage:
                    descText.text = string.Format(itemData.itemDesc, itemData.upgradeDamages[level]);
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
                case ItemData.ItemType.Heal:
                    descText.text = string.Format(itemData.itemDesc);
                    break;


            }
        }
    }
    #endregion

    /// <summary>
    /// UIȰ��ȭ ���¿��� ������ ���� ��, ������ ������ ���׷��̵�
    /// </summary>
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
                else if(level > 5)
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.BaseRange;

                    nextDamage = weapon.damage + (itemData.upgradeDamages[level] / 10);
                    nextRange = weapon.range * (1 + (itemData.upgradeRange[level] / 10));

                    weapon.LevelUp(nextDamage, nextRange);
                }
                else
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.BaseRange;

                    nextDamage = weapon.damage + itemData.upgradeDamages[level];
                    nextRange = weapon.BaseRange * (1 + itemData.upgradeRange[level]);

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
                else if (level > 5)
                {
                    float nextDamage = weapon.damage;

                    nextDamage = weapon.damage + (itemData.upgradeDamages[level] / 10);

                    weapon.LevelUp(nextDamage, 1);
                }
                else
                {
                    float nextDamage = weapon.damage;

                    nextDamage = weapon.damage +itemData.upgradeDamages[level];

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
                else if (level > 5)
                {
                    float nextPassiveValue = (itemData.upgradeDamages[level] / 10);

                    passive.LevelUp(nextPassiveValue * level);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeDamages[level];

                    passive.LevelUp(nextPassiveValue * level);
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
                else if (level > 5)
                {
                    float nextPassiveValue = (itemData.upgradeRange[level] / 10);

                    passive.LevelUp(nextPassiveValue * level);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeRange[level];

                    passive.LevelUp(nextPassiveValue * level);
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
                else if (level > 5)
                {
                    float nextPassiveValue = (itemData.upgradeCT[level] / 10);

                    passive.LevelUp(nextPassiveValue * level);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeCT[level];

                    passive.LevelUp(nextPassiveValue * level);
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
                else if (level > 5)
                {
                    float nextPassiveValue = (itemData.upgradeMoveSpeed[level] / 10);

                    passive.LevelUp(nextPassiveValue * level);
                }
                else
                {
                    float nextPassiveValue = itemData.upgradeMoveSpeed[level];

                    passive.LevelUp(nextPassiveValue * level);
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
                else if (level > 5)
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.BaseRange;

                    nextDamage = weapon.damage + (itemData.upgradeDamages[level] / 10);
                    nextRange = weapon.BaseRange * (1 + (itemData.upgradeRange[level] / 10));

                    weapon.LevelUp(nextDamage, nextRange);
                }
                else
                {
                    float nextDamage = weapon.damage;
                    float nextRange = weapon.BaseRange;

                    nextDamage = weapon.damage + itemData.upgradeDamages[level];
                    nextRange = weapon.BaseRange * (1 + itemData.upgradeRange[level]);

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
                else if (level > 5)
                {
                    float nextDamage = weapon.damage;

                    nextDamage = weapon.damage + (itemData.upgradeDamages[level] / 10);


                    weapon.LevelUp(nextDamage, 6);
                }
                else
                {
                    float nextDamage = weapon.damage;

                    nextDamage = weapon.damage + itemData.upgradeDamages[level];

                    weapon.LevelUp(nextDamage, 6);
                }
                iconObj[3].SetActive(true);
                Text objCross = iconObj[3].GetComponentInChildren<Text>();
                objCross.text = "Lv" + (level + 1);
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.hp = GameManager.instance.MaxHp;
                break;


        }

        level++;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        
    }
}
