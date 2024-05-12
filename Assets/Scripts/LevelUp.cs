using UnityEngine;

/// <summary>
/// 플레이어 레벨업 시 UI호출
/// </summary>
public class LevelUp : MonoBehaviour
{
    //자식 UI 판넬 목록
    private Item[] items;
    
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    /// <summary>
    /// 아이템 선택
    /// </summary>
    /// <param name="index"></param>
    public void Select(int index)
    {
        items[index].OnClick();
    }

    /// <summary>
    /// 레벨업 시 UI호출
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
    /// 레벨업 시 중복되지않는 랜덤 아이템 표시
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
