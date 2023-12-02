using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogData; // 대화 내용 저장
    Dictionary<int, Sprite> portraitData; // 초상화 목록 저장

    public Sprite[] portraitArr; // 초상화 Sprite 꺼내오기 위한 배열

    void Start()
    {

    }

    private void Awake()
    {
        // 초기화
        dialogData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        // 데이터 생성 함수 호출
        GenerateData();
    }

    // 대사 & 초상화 생성 함수
    void GenerateData()
    {
        dialogData.Add(100, new string[] {  // 조종대
            "이동: WASD 또는 방향키\n달리기: shift\n아이템 상호작용: e 또는 마우스\n인벤토리 선택: 숫자 1 ~ 5/6",
            "오늘도 아무런 연락이 없으시네..\n교수님은 대체 언제 오시는거지?/4", 
            "설마 진짜로 무슨 일이라도 생기신 거 아니야?/3", "나.. 졸업은 할 수 있을까.../1",
            "진짜 직접 찾으러 가봐야 하나...(언짢)/5",
            "에휴 졸업하려면 어쩔 수 없지\n일단 교수님 통신이 어디서부터 끊겼는지 확인해봐야겠어/2"
        });
        dialogData.Add(200, new string[] { // 칠판
            "1. 온실로 가서 물 주기\n2. 교수님과의 통신 확인하기/6",
            "일단 온실에 가서 물을 먼저 줄까?/0"
        });
        dialogData.Add(300, new string[] { // 온실 문
            "이제 조종실로 가서 통신을 확인해보자/0"
        });

        //portraitData.Add(100 + 0, portraitArr[0]); // normal face
        //portraitData.Add(100 + 1, portraitArr[1]); // closed eye
        //portraitData.Add(100 + 2, portraitArr[2]); // stare face
        //portraitData.Add(100 + 3, portraitArr[3]); // side eye
        //portraitData.Add(100 + 4, portraitArr[4]); // little annoying
        //portraitData.Add(100 + 5, portraitArr[5]); // very annoying

        //portraitData.Add(200 + 0, portraitArr[0]); // normal face
        //portraitData.Add(200 + 1, portraitArr[1]); // closed eye
        //portraitData.Add(200 + 2, portraitArr[2]); // stare face
        //portraitData.Add(200 + 3, portraitArr[3]); // side eye
        //portraitData.Add(200 + 4, portraitArr[4]); // little annoying
        //portraitData.Add(200 + 5, portraitArr[5]); // very annoying

        portraitData.Add(0, portraitArr[0]); // normal face
        portraitData.Add(1, portraitArr[1]); // closed eye
        portraitData.Add(2, portraitArr[2]); // stare face
        portraitData.Add(3, portraitArr[3]); // side eye
        portraitData.Add(4, portraitArr[4]); // little annoying
        portraitData.Add(5, portraitArr[5]); // very annoying
        portraitData.Add(6, portraitArr[6]); // 보여지지 않을 초상화
    }

    // 대화 반환 함수
    public string GetDialog(int id, int dialogIndex) // 대사 리턴 (id = 대화상대 객체id , dialogIndex = 대사 인덱스)
    {
        if (dialogIndex == dialogData[id].Length) // 대화가 끝나면
            return null; // 종료
        else // 대사가 남아있으면
            return dialogData[id][dialogIndex]; // 대사 반환
    }

    //public Sprite GetPortrait(int id, int portraitIndex)
    //{
    //    return portraitData[id + portraitIndex]; // 초상화 이미지 반환
    //}
    public Sprite GetPortrait(int portraitIndex) // 초상화 이미지 리턴 (portraitIndex == 초상화 번호)
    {
        return portraitData[portraitIndex]; // 초상화 이미지 반환
    }
}
