using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 텍스트에 플래쉬효과 부여
/// </summary>
public class FlashText : MonoBehaviour
{
    private Text text;
    private string saveText;

    private void Awake()
    {
        text = GetComponent<Text>();
        saveText = text.text;
        StartCoroutine(Flash());
    }

    /// <summary>
    /// 텍스트 점멸 효과
    /// </summary>
    IEnumerator Flash()
    {
        while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(0.3f);
            text.text = saveText;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
