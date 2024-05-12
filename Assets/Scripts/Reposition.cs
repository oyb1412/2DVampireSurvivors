using UnityEngine;

/// <summary>
/// �÷��̾� �̵��� ���� �� �� �ֳʹ� �����̵�
/// </summary>
public class Reposition : MonoBehaviour
{
    //������Ʈ�� �浹���� ����� ȣ��Ǵ� �Լ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ÿ�ϸʰ� ������(�÷��̾� �ֺ� ����)���� �浹�� �ƴϸ� ����
        if (!collision.CompareTag("Area"))
            return;

        //Ÿ�ϸ��� �̵� ��ġ�� ���ϱ� ���� ���� ������ ����Ѵ�.
        //�÷��̾� ��ġ ����
        Vector2 playerPos = GameManager.instance.player.transform.position;

        //Ÿ�ϸ� ��ġ ����
        Vector2 tilePos = transform.position;

        //�÷��̾�� Ÿ�ϰ��� �Ÿ��� ���� x,y�� ���밪���� ����
        Vector2 curDiff;
        curDiff.x = Mathf.Abs(playerPos.x - tilePos.x);
        curDiff.y = Mathf.Abs(playerPos.y - tilePos.y);

        //�÷��̾� ���� ����
        Vector2 playerVec = GameManager.instance.player.InputVec;

        //�÷��̾� ������ �ٰŷ� Ÿ�ϸ��� �̵���� �����¿츦 1,-1�� ����
        float dirX = playerVec.x > 0 ? 1 : -1;
        float dirY = playerVec.y > 0 ? 1 : -1;

        //����� ������ ���� Ÿ�ϸ��� ��ġ�� ��ȯ�Ѵ�.
        switch(transform.tag)
        {
            //� �±��� ������Ʈ�� �������� ����
            //�±װ� �׶���(Ÿ�ϸ�)�϶�
            case "Ground":
                //�÷��̾ Ÿ�ϸ��� x�� �������� Ż���Ϸ� �� ��
                if(curDiff.x > curDiff.y)
                {
                    //Ÿ���� x�� �������� Ÿ�ϸ�ũ��*2��ŭ �̵�
                    transform.Translate(new Vector3(dirX * 40f, 0f, 0f));
                }
                //�÷��̾ Ÿ�ϸ��� y�� �������� Ż���Ϸ� �� ��
                if (curDiff.y > curDiff.x)
                {
                    //Ÿ���� y�� �������� Ÿ�ϸ�ũ��*2��ŭ �̵�
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
