using UnityEngine;

/// <summary>
/// 게임 종료 시 UI호출
/// </summary>
public class Restart : MonoBehaviour
{
    [SerializeField]private GameObject[] titles;

    /// <summary>
    /// 게임 패배 시 패배UI호출
    /// </summary>
    public void Lose()
    {
        titles[0].SetActive(true);
    }

    /// <summary>
    /// 게임 승리 시 패배UI호출
    /// </summary>
    public void Win()
    {
        titles[1].SetActive(true);
    }
}
