using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ؽ�Ʈ�� �÷���ȿ�� �ο�
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
    /// �ؽ�Ʈ ���� ȿ��
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
