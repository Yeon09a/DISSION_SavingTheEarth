using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiddleOctMonster : MonoBehaviour
{
    public Monster octMon;
    
    public GameObject[] beams;
    public GameObject[] bubbles;

    /*private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f) };
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f) };*/


    // 방울 발사 위치 정하기 
    private int[] bubbleRot = {-50, -35, -18, 0, 18, 35, 50}; // inspector로 설정

    public static bool isAttaking; // 공격 중인지 확인

    // 빔 개수
    private int maxBeam = 8; // 임시
    private int maxBubble = 7; // 임시

    public Action startBeamAnimation;
    public Action startBubbleMove;

    // 공격 번호
    private int attackNum; // 1, 2, 3

    // Start is called before the first frame update
    void Start()
    {

        
    }

    IEnumerator Attack() // 공격 함수
    {
        while (true)
        {
            attackNum = Random.Range(1, 3);
            if (attackNum == 1)
            {
                ShootBeam();
            }
            else if (attackNum == 2)
            {
                ShootBubble();
            }

            yield return new WaitForSeconds(6.0f);
        }
    }

    private void ShootBeam() // 빔 발사 함수
    {
        Debug.Log("먹물 기둥");
        for (int i = 0; i < maxBeam; i++)
        {
            beams[i].SetActive(true); // 위치는 후에 맵에 맞춰서 수정
        }

        startBeamAnimation();
    }

    private void ShootBubble()
    {
        Debug.Log("먹물 방울");
        for (int i = 0; i < maxBubble; i++)
        {
            bubbles[i].SetActive(true);
        }
        startBubbleMove();
    }

    public void StartAttack()
    {
        StartCoroutine(Attack());
    }


}
