using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelUp : MonoBehaviour
{
    public Item[] items;
    RectTransform rect;
    // Start is called before the first frame update
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        RandomItem();
        AudioManager.instance.OnEffect(true);
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        AudioManager.instance.OnEffect(false);
        GameManager.instance.ReStart();
    }

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

            if (item.level == 5)
            {
                items[4].gameObject.SetActive(true);            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
