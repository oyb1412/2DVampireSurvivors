using UnityEngine;

/// <summary>
/// �÷��̾� ������ �� UIȣ��
/// </summary>
public class LevelUp : MonoBehaviour
{
    //�ڽ� UI �ǳ� ���
    private Item[] items;
    
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="index"></param>
    public void Select(int index)
    {
        items[index].OnClick();
    }

    /// <summary>
    /// ������ �� UIȣ��
    /// </summary>
    public void Show()
    {
        rect.localScale = Vector3.one;
        RandomItem();
        AudioManager.instance.OnEffect(true);
        GameManager.instance.Stop();
    }

    /// <summary>
    /// UI Hide
    /// </summary>
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        AudioManager.instance.OnEffect(false);
        GameManager.instance.ReStart();
    }

    /// <summary>
    /// ������ �� �ߺ������ʴ� ���� ������ ǥ��
    /// </summary>
    public void RandomItem()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
                break;
        }

        for (int i = 0; i<ran.Length; i++)
        {
            Item item = items[ran[i]];
            item.gameObject.SetActive(true);
        }
    }
}
