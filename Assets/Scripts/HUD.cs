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
                //처치 횟수 텍스트로 표시
                myText.text = "Kill : " + (GameManager.instance.kill.ToString());
            break;
            case infoType.hp:
                //플레이어 포지션
                Vector3 playerPos = GameManager.instance.player.transform.position;
                //플레이어의 현재 체력상태 / 최대 체력상태를 계산하여 슬라이드의 %를 계산
                mySlider.value = GameManager.instance.hp / GameManager.instance.maxHp;
                //체력바의 포지션을 플레이어의 world포지션으로 변경해준다.
                myRect.position = Camera.main.WorldToScreenPoint(new Vector3(playerPos.x,playerPos.y + 0.7f,playerPos.z));
            break;
            case infoType.exp:
                //현재 exp
                float curExp = GameManager.instance.exp;
                //최대 exp
                float maxExp = GameManager.instance.maxExp[GameManager.instance.level];
                //슬라이더 비율
                mySlider.value = curExp / maxExp;
            break;
            case infoType.time:
                //현재 시간(초)
                float curTime = GameManager.instance.curTime;
                //현재 시간(분)
                float minTime = GameManager.instance.minTime;
                //분,초를 최소 2자릿수로 텍스트에 표시
                myText.text = string.Format("{0:D2} : {1:D2}", Convert.ToInt32(minTime), Convert.ToInt32(curTime));
            break;
            case infoType.level:
                //현재 레벨 텍스트로 표시
                myText.text = "Level : " + GameManager.instance.level.ToString();
                break;
        }
    }
}
