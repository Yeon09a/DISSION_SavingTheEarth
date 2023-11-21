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
        dialogData.Add(100, new string[] { "이동: WASD 또는 방향키\n달리기: shift\n아이템 상호작용: e 또는 마우스\n인벤토리 선택: 숫자 1 ~ 5" });
        dialogData.Add(200, new string[] { "1. 온실로 가서 물 주기\n2. 교수님과의 통신 확인하기" });

        // portraitData.Add(~~~(id) + n, portraitArr[n]); // 후에 수정
    }

    // 대화 반환 함수
    public string GetDialog(int id, int dialogIndex) // 대사 리턴 (id = 대화상대 객체id , dialogIndex = 대사 인덱스)
    {
        if (dialogIndex == dialogData[id].Length) // 대화가 끝나면
            return null; // 종료
        else // 대사가 남아있으면
            return dialogData[id][dialogIndex]; // 대사 반환
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex]; // 초상화 이미지 반환
    }
}
