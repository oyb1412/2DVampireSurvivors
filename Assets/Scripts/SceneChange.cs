using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 전환 
/// </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// 씬 전환(버튼으로 호출)
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
