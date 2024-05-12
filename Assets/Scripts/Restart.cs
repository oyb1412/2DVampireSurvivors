using UnityEngine;

/// <summary>
/// ���� ���� �� UIȣ��
/// </summary>
public class Restart : MonoBehaviour
{
    [SerializeField]private GameObject[] titles;

    /// <summary>
    /// ���� �й� �� �й�UIȣ��
    /// </summary>
    public void Lose()
    {
        titles[0].SetActive(true);
    }

    /// <summary>
    /// ���� �¸� �� �й�UIȣ��
    /// </summary>
    public void Win()
    {
        titles[1].SetActive(true);
    }
}
