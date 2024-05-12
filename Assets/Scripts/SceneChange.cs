using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �� ��ȯ 
/// </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// �� ��ȯ(��ư���� ȣ��)
    /// </summary>
    public void GoGameScene()
    {
        StartCoroutine(Sound());
    }

    IEnumerator Sound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
