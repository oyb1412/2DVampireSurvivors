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
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
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
        if (!GameManager.instance.isLive)
            return;

        switch (type)
        {
            case infoType.kill:
                //óġ Ƚ�� �ؽ�Ʈ�� ǥ��
                myText.text = GameManager.instance.kill.ToString();
            break;
            case infoType.hp:

                //�÷��̾��� ���� ü�»��� / �ִ� ü�»��¸� ����Ͽ� �����̵��� %�� ���
                mySlider.value = GameManager.instance.hp / GameManager.instance.maxHp;

            break;
            case infoType.exp:
                //���� exp
                float curExp = GameManager.instance.exp;
                //�ִ� exp
                float maxExp = GameManager.instance.maxExp[Mathf.Min(GameManager.instance.level, GameManager.instance.maxExp.Length -1 )];
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
                myText.text = "Lv : " + GameManager.instance.level.ToString();
                break;
        }
    }
}
