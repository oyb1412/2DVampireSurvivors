using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �⺻ GUI ����
/// </summary>
public class HUD : MonoBehaviour
{
    public enum infoType
    {
        kill,
        hp,
        exp,
        time,
        level
    }

    public infoType type;
    private Text myText;
    private Slider mySlider;
    private RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        switch (type)
        {
            case infoType.hp:
                //�÷��̾� ������
                Vector3 playerPos = GameManager.instance.player.transform.position;
                //ü�¹��� �������� �÷��̾��� world���������� �������ش�.
                myRect.position = Camera.main.WorldToScreenPoint(new Vector3(playerPos.x, playerPos.y + 1.0f, playerPos.z));
                break;
        }
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        switch (type)
        {
            case infoType.kill:
                //óġ Ƚ�� �ؽ�Ʈ�� ǥ��
                myText.text = GameManager.instance.KillNumber.ToString();
            break;
            case infoType.hp:

                //�÷��̾��� ���� ü�»��� / �ִ� ü�»��¸� ����Ͽ� �����̵��� %�� ���
                mySlider.value = GameManager.instance.hp / GameManager.instance.MaxHp;

            break;
            case infoType.exp:
                //���� exp
                float curExp = GameManager.instance.exp;
                //�ִ� exp
                float maxExp = GameManager.instance.MaxExp[Mathf.Min(GameManager.instance.Level, GameManager.instance.MaxExp.Length -1 )];
                //�����̴� ����
                mySlider.value = curExp / maxExp;
            break;
            case infoType.time:
                //���� �ð�(��)
                float curTime = GameManager.instance.CurrentTime;
                //���� �ð�(��)
                float minTime = GameManager.instance.MinTime;
                //��,�ʸ� �ּ� 2�ڸ����� �ؽ�Ʈ�� ǥ��
                myText.text = string.Format("{0:D2} : {1:D2}", Convert.ToInt32(minTime), Convert.ToInt32(curTime));
            break;
            case infoType.level:
                //���� ���� �ؽ�Ʈ�� ǥ��
                myText.text = "Lv : " + GameManager.instance.Level.ToString();
                break;
        }
    }
}
