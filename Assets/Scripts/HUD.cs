using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Text myText;
    Slider mySlider;
    RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case infoType.kill:
                //óġ Ƚ�� �ؽ�Ʈ�� ǥ��
                myText.text = "Kill : " + (GameManager.instance.kill.ToString());
            break;
            case infoType.hp:
                //�÷��̾� ������
                Vector3 playerPos = GameManager.instance.player.transform.position;
                //�÷��̾��� ���� ü�»��� / �ִ� ü�»��¸� ����Ͽ� �����̵��� %�� ���
                mySlider.value = GameManager.instance.hp / GameManager.instance.maxHp;
                //ü�¹��� �������� �÷��̾��� world���������� �������ش�.
                myRect.position = Camera.main.WorldToScreenPoint(new Vector3(playerPos.x,playerPos.y + 0.7f,playerPos.z));
            break;
            case infoType.exp:
                //���� exp
                float curExp = GameManager.instance.exp;
                //�ִ� exp
                float maxExp = GameManager.instance.maxExp[GameManager.instance.level];
                //�����̴� ����
                mySlider.value = curExp / maxExp;
            break;
            case infoType.time:
                //���� �ð�(��)
                float curTime = GameManager.instance.curTime;
                //���� �ð�(��)
                float minTime = GameManager.instance.minTime;
                //��,�ʸ� �ּ� 2�ڸ����� �ؽ�Ʈ�� ǥ��
                myText.text = string.Format("{0:D2} : {1:D2}", Convert.ToInt32(minTime), Convert.ToInt32(curTime));
            break;
            case infoType.level:
                //���� ���� �ؽ�Ʈ�� ǥ��
                myText.text = "Level : " + GameManager.instance.level.ToString();
                break;
        }
    }
}
