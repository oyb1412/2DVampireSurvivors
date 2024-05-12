using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Status")]
    public float range;
    public float damage;
    public float coolTime;

    private int id;
    private int prefabId;
    private int count =1;
    private int weaponType;
    private float timer;
    private float chargeTimer;
    private bool chargeTrigger;
    public float BaseRange { get; private set; }
    public float BaseCoolTime { get; private set; }
    public float BaseDamage { get; private set; }
    /// <summary>
    /// 무기 초기화
    /// </summary>
    /// <param name="data"></param>
    public void Init(ItemData data) {
        name = "Weapon" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.damage / 10;
        count = data.count;
        range = data.range / 100;
        coolTime = data.CT / 100;

        BaseRange = range;
        BaseCoolTime = coolTime;
        BaseDamage = damage;
        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++) {
            if (data.weaponObject == GameManager.instance.pool.prefabs[i]) {
                prefabId = i;
                break;
            }
        }

        weaponType = (int)data.itemType;
        switch (id) {
            case 7:
                Assign();
                break;
        }

        GameManager.instance.player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// 각 무기에 따른 동작
    /// </summary>
    private void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        switch (id)
        {
            case 0:
                if (Input.GetMouseButton(0) && timer > coolTime)
                {
                    if (chargeTimer < 10)
                        chargeTimer += Time.deltaTime * 5;

                    if(chargeTimer > 0.5f)
                        GameManager.instance.player.ChargeaAnimation(true);


                    chargeTrigger = true;
                }
                if (Input.GetMouseButtonUp(0) && timer > coolTime)
                {
                    GameManager.instance.player.ChargeaAnimation(false);
                    chargeTrigger = false;

                    timer = 0;
                    FireMoonSlash();
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Sword);

                }
                if (!chargeTrigger)
                    timer += Time.deltaTime;

                break;
            case 1:
                timer += Time.deltaTime;
                if(timer > coolTime)
                {
                    timer = 0;
                    FireDagger();
                }
                break;
            case 7:
                AutoRotate();
                break;
            case 8:
                timer += Time.deltaTime;
                if(timer > coolTime)
                {
                    timer = 0;
                    FireCross();
                }
                break;
        }
    }

    /// <summary>
    /// MoonShash 발사
    /// </summary>
    void FireMoonSlash()
    {
        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mouse - playerPos;
        dir = dir.normalized;
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = GameObject.Find("Weapon0").transform;
        bullet.transform.position = GameManager.instance.player.transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.transform.localScale = Vector3.one * (range + chargeTimer);
        bullet.GetComponent<Bullet>().Init(damage + chargeTimer * 15, id, dir, count);
        chargeTimer = 1;

    }

    /// <summary>
    /// Dagger 발사
    /// </summary>
    void FireDagger()
    {
        //스캐너에 걸린 애너미가 없으면 함수 종료
        if (!GameManager.instance.player.Scanner.Target)
            return;
        //플레이어 포지션
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //스캐너에 걸린 애너미 위치 
        Vector3 targetPos = GameManager.instance.player.Scanner.Target.position;
        //플레이어->애너미 벡터 저장후 정규화
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //풀 매니저에 새롭게 자식오브젝트로 생성
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //불렛 오브젝트를 웨폰1 오브젝트의 하위로 이동
        bullet.parent = GameObject.Find("Weapon1").transform;
        //불렛 위치 초기화
        bullet.position = playerPos;
        //불렛의 쿼터니온 회전
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, vecDir);

        bullet.localScale = Vector3.one * range;
        //데미지와 관통력, 발사 방향 지정
        bullet.GetComponent<Bullet>().Init(damage, weaponType, vecDir, count);
    }

    /// <summary>
    /// Cross 발사
    /// </summary>
    void FireCross()
    {
        //스캐너에 걸린 애너미가 없으면 함수 종료
        if (!GameManager.instance.player.Scanner.Target)
            return;
        //플레이어 포지션
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //스캐너에 걸린 애너미 위치 
        Vector3 targetPos = GameManager.instance.player.Scanner.Target.position;
        //플레이어->애너미 벡터 저장후 정규화
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //풀 매니저에 새롭게 자식오브젝트로 생성
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //불렛 오브젝트를 웨폰1 오브젝트의 하위로 이동
        bullet.parent = GameObject.Find("Weapon8").transform;
        //불렛 위치 초기화
        bullet.position = playerPos;
        bullet.localScale = Vector3.one * range;
        bullet.GetComponent<Bullet>().Init(damage, id, vecDir, count);

    }

    /// <summary>
    /// 레벨업 시 웨펀 능력치 증가
    /// </summary>
    public void LevelUp(float damage, float range)
    {
        this.damage = damage;
        this.range = range;
        count++;

        if (id == 7)
            Assign();

        GameManager.instance.player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
    }

    //무기 생성, 배치
    void Assign()
    {
        //무기의 갯수만큼 반복 실행
        for(int i = 0; i< count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                //그 자식 오브젝트를 그대로 사용
                bullet = transform.GetChild(i);
            }
            //레벨업으로 인해 새롭게 배치를 진행할 경우
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            //생성직후 위치 0,0,0으로 초기화
            bullet.transform.position = GameManager.instance.player.transform.position;

            //생성직후 회전량 초기화
            bullet.transform.rotation = Quaternion.identity;

            //z축을 기준으로 360 * index * count로 각도 조정
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //각도 조정 후 y축으로 위치이동
            bullet.transform.Translate(0, 1.5f, 0f);

            bullet.localScale = Vector3.one * range;

            //불렛의 초기화 진행
            bullet.GetComponent<Bullet>().Init(damage, id, Vector3.zero, count);
        }
    }

    //무기 자동회전
    void AutoRotate()
    {
           //z축을 기준으로 속도만큼 자동회전
           transform.Rotate(Vector3.forward , (300f - (100 - (coolTime * 10))) * Time.deltaTime);
    }
}
