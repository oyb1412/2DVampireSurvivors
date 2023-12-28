using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoGameScene()
    {
        StartCoroutine(Sound());
    }

    IEnumerator Sound()
    {
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Select);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
