using UnityEngine;

/// <summary>
/// 플레이어 이동에 따른 맵 및 애너미 강제이동
/// </summary>
public class Reposition : MonoBehaviour
{
    //오브젝트간 충돌에서 벗어날때 호출되는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        //타일맵과 에리어(플레이어 주변 영역)간의 충돌이 아니면 무시
        if (!collision.CompareTag("Area"))
            return;

        //타일맵의 이동 위치를 정하기 위해 각종 정보를 취득한다.
        //플레이어 위치 저장
        Vector2 playerPos = GameManager.instance.player.transform.position;

        //타일맵 위치 저장
        Vector2 tilePos = transform.position;

        //플레이어와 타일간의 거리를 각각 x,y로 절대값으로 저장
        Vector2 curDiff;
        curDiff.x = Mathf.Abs(playerPos.x - tilePos.x);
        curDiff.y = Mathf.Abs(playerPos.y - tilePos.y);

        //플레이어 방향 저장
        Vector2 playerVec = GameManager.instance.player.InputVec;

        //플레이어 방향을 근거로 타일맵의 이동경로 상하좌우를 1,-1로 저장
        float dirX = playerVec.x > 0 ? 1 : -1;
        float dirY = playerVec.y > 0 ? 1 : -1;

        //취득한 정보를 토대로 타일맵의 위치를 변환한다.
        switch(transform.tag)
        {
            //어떤 태그의 오브젝트를 변경할지 결정
            //태그가 그라운드(타일맵)일때
            case "Ground":
                //플레이어가 타일맵의 x축 방향으로 탈출하려 할 때
                if(curDiff.x > curDiff.y)
                {
                    //타일을 x축 방향으로 타일맵크기*2만큼 이동
                    transform.Translate(new Vector3(dirX * 40f, 0f, 0f));
                }
                //플레이어가 타일맵의 y축 방향으로 탈출하려 할 때
                if (curDiff.y > curDiff.x)
                {
                    //타일을 y축 방향으로 타일맵크기*2만큼 이동
                    transform.Translate(new Vector3(0f, dirY * 40f, 0f));
                }
                break;
            case "Enemy":
                if (curDiff.x > curDiff.y)
                {
                    transform.Translate(new Vector3(dirX * 25f + Random.Range(-3f,3f), Random.Range(-3f, 3f), 0f));
                }
                if (curDiff.y > curDiff.x)
                {
                    transform.Translate(new Vector3(Random.Range(-3f, 3f), dirY * 25f + Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
