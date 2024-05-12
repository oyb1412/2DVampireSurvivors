using UnityEngine;

public class Scanner : MonoBehaviour
{
    //스캔 범위
    [SerializeField]private float scanRange;
    //스캔할 대상의 레이어마스크
    [SerializeField] private LayerMask layer;
    //레이캐스트가 히트한 대상(스캔한 대상)을 저장할 배열
    private RaycastHit2D[] targets;
    //가장 가까운 대상
    public Transform Target { get; private set; }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        //원형의 캐스트를 쏘고 모든 결과를 반환하는 함수 (1.캐스팅 시작 위치 2.원의 반지름 3.캐스팅 방향 4.쏘는 방향의 길이 5.대상 레이어 )
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, layer);
        Target = GetTarget();
    }

    /// <summary>
    /// 가장 가까운 적을 반환
    /// </summary>
    /// <returns>가장 가까운 적</returns>
    Transform GetTarget()
    {
        Transform result = null;
        //스캔 범위 선언
        float diff = 100f;
        //스캔된 모든 적의 수만큼 반복문 실행
        for(int i = 0; i<targets.Length; i++)
        {
            //플레이어 포지션
            Vector2 playerPos = transform.position;
            //애너미의 포지션
            Vector2 enemyPos = targets[i].transform.position;
            //벡터 a,b의 거리를 반환하는 함수
            float curDiff = Vector2.Distance(playerPos, enemyPos);
            //지정된 범위안에 애너미가 존재할시
            if (curDiff < diff)
            {
                //그 애너미와의 거리를 새롭게 지정
                diff = curDiff;
                //그 애너미를 가장 가까운 타겟으로 지정
                result = targets[i].transform;
            }

        }

        return result;
    }    
}
