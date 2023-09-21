using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Character
{
    
    [Header ("BossInfo")]
    public GameObject beam;
    private int beamCount = 0;
    public GameObject[] etcMons;
    public GameObject spawnCircle;

    // 임시
    private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f)}; // 맵 나오면 수정
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f)}; // 맵 나오면 수정

    public static bool isAttaking; // 공격 중인지 확인

    public delegate void bossDel();
    public static event bossDel startBeamAnimation;
    private event bossDel spawnEtcMons;

    // 공격 관련 변수
    private int maxBeam = 8; // 빔 개수(임시)
    private int attackNum; // 공격 번호(1, 2, 3)
    private int prevAttactNum; // 이전 공격 번호
    private int attackCount = 0; // 공격 횟수 
    private float paralysisTime = 10.0f;
    private bool isHpHalf = false; // 몬스터 hp가 반 이하인지 체크
    private bool isShield = false;
    private int maxEtcMons = 8;
    private int etcMonsCount = 0;


    private Player player;

    // 몬스터 정보
    public Monster bossMonster;

    // Start is called before the first frame update
    void Start()
    {
        hp = bossMonster.hp;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        StartCoroutine(Attack());
    }

    protected override void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            attackNum = isHpHalf ? Random.Range(1, 4) : Random.Range(1, 3);
            if (attackNum == 1 || prevAttactNum == 2)
            {
                prevAttactNum = 1;
                ShootBeam();
            }
            else if (attackNum == 2)
            {
                prevAttactNum = 2;
                StartCoroutine(Paralysis());
            }
            else if (attackNum == 3)
            {
                prevAttactNum = 3;
                MakeShield();
            }

            attackCount++;

            if (attackCount < 5 && attackNum != 3)
            {
                yield return new WaitForSeconds(6.0f);
            } else if (attackCount == 5){ // 공격 5번 쿨타임
                attackCount = 0;
                yield return new WaitForSeconds(12.0f);
            } else // 공격 3일때 
            {
                yield return new WaitWhile(() => isShield);
                spawnEtcMons = null;
            }
        }
    }

    private void ShootBeam() // 빔 공격
    {
        Debug.Log("공격1");
        for (int i = 0; i < maxBeam; i++)
        {
            Instantiate(beam, setPos(i), Quaternion.identity); // 위치는 후에 맵에 맞춰서 수정
        }

        startBeamAnimation();
    }

    IEnumerator Paralysis() // 마비 공격
    {
        Debug.Log("공격2");
        if (player.playerDir == PlayerDir.Up)
        {
            Debug.Log("마비 시작");
            player.SetSpeed(0.0f);
           // player.SetAnimator(false);

            yield return new WaitForSeconds(paralysisTime);

            Debug.Log("마비 끝");
            player.SetSpeed(5.0f);
           // player.SetAnimator(true);
        }
    }

    private void MakeShield()
    {
        isShield = true;
        for(int i = 0; i < maxEtcMons; i++)
        {
            spawnEtcMons += () => StartCoroutine(SpawnEtcMons(i, etcMons[Random.Range(1, 3)]));
        }

        spawnEtcMons();
    }

    // 빔 위치 설정 함수
    private Vector2 setPos(int i)
    {
        Vector2 pos = new Vector2(0f, 0f);

        if(i % 4 == 0)
        {
            pos.x = Random.Range(boundarysX[0].x, boundarysX[0].y);
        }
        else if (i % 4 == 1)
        {
            pos.x = Random.Range(boundarysX[1].x, boundarysX[1].y);
        } 
        else if (i % 4 == 2)
        {
            pos.x = Random.Range(boundarysX[2].x, boundarysX[2].y); 
        }
        else if (i % 4 == 3)
        {
            pos.x = Random.Range(boundarysX[3].x, boundarysX[3].y);
        }

        if(i < 4)
        {
            pos.y = Random.Range(boundarysY[0].x, boundarysY[0].y);
        } 
        else {
            pos.y = Random.Range(boundarysY[1].x, boundarysY[1].y);
        }


        return pos;
    }


    // 체력 감소
    public void DecreaseHp(float attack)
    {
        if(!isShield)
        {
            hp -= attack;
            if (hp < bossMonster.hp / 2.0f)
            {
                isHpHalf = true;
            }
        }
    }

    // etcMons 생성
    IEnumerator SpawnEtcMons(int i, GameObject etcMon)
    {
        GameObject circle = Instantiate(spawnCircle, setPos(i), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GameObject mon = Instantiate(etcMon, setPos(i), Quaternion.identity);
        mon.AddComponent<EtcMonster>();
        etcMonsCount++;
        yield return new WaitForSeconds(0.5f);
        Destroy(circle);
    }

    // etcMons 수 계산
    public void CalEtcMonsCount()
    {
        etcMonsCount--;
        if(etcMonsCount == 0)
        {
            isShield = false;
        }
    }
}
