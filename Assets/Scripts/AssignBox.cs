using UnityEngine;

/// <summary>
/// 게임 시작 시 Box 생성
/// </summary>
public class AssignBox : MonoBehaviour
{
    /// <summary>
    /// 게임 시작시 랜덤 위치에 박스 생성
    /// </summary>
    /// <param name="index">생성할 갯수</param>
    public void BoxInit(int index)
    {
        for (int i = 0; i < index; i++)
        {
            Vector3 ranPos = new Vector3(Random.Range(-40f, 40f), Random.Range(-25f, 25), 0f);
            Transform box = GameManager.instance.pool.Get(7).transform;
            box.position = ranPos;
            box.parent = GameObject.Find("BoxManager").transform;
        }
    }
}
