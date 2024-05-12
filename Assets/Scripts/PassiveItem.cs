using UnityEngine;

/// <summary>
/// �������Ǵ� �нú� ������ ������ ����
/// </summary>
public class PassiveItem : MonoBehaviour
{
    [SerializeField]private ItemData.ItemType itemDate;
    private float value;

    /// <summary>
    /// �нú� ������ ù ȹ��� �ʱ�ȭ
    /// </summary>
    /// <param name="data"></param>
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

    #region Application

    /// <summary>
    /// �нú� ������ ���� �� �� ������ ����
    /// </summary>
    public void LevelUp(float value)
    {
        this.value = value;
        ApplyPassive();
    }

    /// <summary>
    /// ��Ÿ�� ����
    /// </summary>
    void CoolDown()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            weapon.coolTime = weapon.BaseCoolTime * (1 - value);
        }
    }

    /// <summary>
    /// �̵��ӵ� ����
    /// </summary>
    void MoveUp()
    {
        GameManager.instance.player.speed = GameManager.instance.player.BaseSpeed * (1 + value);
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    void DamageUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
           weapon.damage = weapon.BaseDamage * (1 + value);
        }
    }

    /// <summary>
    /// ��Ÿ� ����
    /// </summary>
    void RangeUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            float basevalue = 1 + value;
            weapon.range = weapon.BaseRange * (1 + value);
        }
    }

    /// <summary>
    /// �нú� ������ ������
    /// </summary>
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
    #endregion
}
