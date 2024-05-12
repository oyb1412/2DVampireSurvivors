using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 기본 GUI 관리
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
                //플레이어 포지션
                Vector3 playerPos = GameManager.instance.player.transform.position;
                //체력바의 포지션을 플레이어의 world포지션으로 변경해준다.
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
                //처치 횟수 텍스트로 표시
                myText.text = GameManager.instance.KillNumber.ToString();
            break;
            case infoType.hp:

                //플레이어의 현재 체력상태 / 최대 체력상태를 계산하여 슬라이드의 %를 계산
                mySlider.value = GameManager.instance.hp / GameManager.instance.MaxHp;

            break;
            case infoType.exp:
                //현재 exp
                float curExp = GameManager.instance.exp;
                //최대 exp
                float maxExp = GameManager.instance.MaxExp[Mathf.Min(GameManager.instance.Level, GameManager.instance.MaxExp.Length -1 )];
                //슬라이더 비율
                mySlider.value = curExp / maxExp;
            break;
            case infoType.time:
                //현재 시간(초)
                float curTime = GameManager.instance.CurrentTime;
                //현재 시간(분)
                float minTime = GameManager.instance.MinTime;
                //분,초를 최소 2자릿수로 텍스트에 표시
                myText.text = string.Format("{0:D2} : {1:D2}", Convert.ToInt32(minTime), Convert.ToInt32(curTime));
            break;
            case infoType.level:
                //현재 레벨 텍스트로 표시
                myText.text = "Lv : " + GameManager.instance.Level.ToString();
                break;
        }
    }
}
