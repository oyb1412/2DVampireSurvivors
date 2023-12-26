using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public ItemData itemData;
    Text levelText;

    private void Awake()
    {
        Text texts = GetComponentInChildren<Text>();
        levelText = texts;
    }
}
