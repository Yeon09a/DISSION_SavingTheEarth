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
        // 대사&&퀘스트 데이터

        // 오브젝트id => 조종대:1000 칠판: 2000 온실 문: 3000 조종실 책상: 4000 내방 선반: 5000
        // 교수님방 책상 위 사진: 6000 출구: 7000 내방 침대: 8000 교수님 선반: 9000
        // 플레이어 책상: 10000 교수님방 책상: 11000 교수님방 침대: 12000
        // /뒤에 숫자 : 초상화 배정 번호

        // 기본 대사
        dialogData.Add(1000, new string[] {
            "이동: WASD 또는 방향키\n달리기: shift\n아이템 상호작용: e 또는 마우스\n인벤토리 선택: 숫자 1 ~ 5/6"
        });
        dialogData.Add(2000, new string[] {
            "아무리 생각해도 보드 하나로는 부족해\n개인 보드 하나 놔달라고 말해봐야겠군/2"
        });
        dialogData.Add(3000, new string[] {
            "며칠 청소 안했다고 유리가 얼룩 투성이네..\n교수님 안계셔서 다행이다/4"
        });
        dialogData.Add(4000, new string[] {
            "책상 정리 언제 하지../1"
        });
        dialogData.Add(5000, new string[] {
            "최근에.. 책을 읽었던가?/4"
        });
        dialogData.Add(6000, new string[] {
            "웬 사진들이 이렇게 많아?/3"
        });
        dialogData.Add(8000, new string[]
        {
            "다시 눕고 싶지만... 참자/1"
        });
        dialogData.Add(9000, new string[]
        {
            "교수님은 책도 교수님다운 것만 읽으시네../1"
        });
        dialogData.Add(10000, new string[]
        {
            "졸업 논문... 언제 다 쓰지/5"
        });
        dialogData.Add(11000, new string[]
        {
            "교수님 책상도 깨끗하진 않구나..ㅎ/3"
        });
        dialogData.Add(12000, new string[]
        {
            "교수님 침대... 내 침대보다 좋아보여.../4"
        });

        // 스토리 진행 대사
        dialogData.Add(10 + 1000, new string[] {
            "이동: WASD 또는 방향키\n달리기: shift\n아이템 상호작용: e 또는 마우스\n인벤토리 선택: 숫자 1 ~ 5/6",
            "이제 오늘 해야할 일을 확인하자/0"
        });
        dialogData.Add(20 + 2000, new string[] {
            "< 1. 온실로 가서 물 주기 >\n< 2. 교수님과의 통신 확인하기 >/6",
            "일단 온실에 가서 물을 먼저 줄까?/0"
        });
        dialogData.Add(21 + 3000, new string[] {
            "흠 잘 자라고 있군! 기특해라/0",
            "이제 조종실로 가서 통신을 확인해보자/0"
        });
        dialogData.Add(30 + 1000, new string[] {
            "오늘도 아무런 연락이 없으시네..\n\n교수님은 대체 언제 오시는거지?/4",
            "설마 진짜로 무슨 일이라도 생기신 거 아니야?/3", "나.. 졸업은 할 수 있을까.../1",
            "진짜 직접 찾으러 가봐야 하나...;;/5",
            "에휴 졸업하려면 어쩔 수 없지\n\n일단 교수님 통신이 어디서부터 끊겼는지 확인해봐야겠어/2",
            "\"삐삐삐삐 삐삐삐삐\"\n\n\"삐 ----\"\n\n\"통신 추적 완료\"/6",
            "여기는... 인천 사막인가?\n\n가는 길이 조금 험난하겠군/4",
            "어서 떠날 채비를 하자/2",
            "필요한 물품을 체크해보자!/0",
            "1. 식량 -> 온실\n2. 무기 -> 조종실 책상\n3. 침낭 -> 내 방\n4. 미니 잠수함 키 -> 교수님 방 책상/6",
            "흠 온실부터 들러야겠다/0"
        });
        dialogData.Add(40 + 3000, new string[] {
            "식량 오케이!/0",
            "다음은 조종실로 가자/0"
        });
        dialogData.Add(41 + 4000, new string[] {
            "무기도 챙겼고!/0",
            "내 방에 침낭을 뒀었는데 어디에 놨더라?/2"
        });
        dialogData.Add(42 + 5000, new string[] {
            "여기 있었구나\n\n침낭도 오케이!/0",
            "이제 미니 잠수함 키만 챙기면 되겠다/0",
            "근데 교수님 방에 있을 텐데.. 잠깐 들어가봐도 되겠지?/2"
        });
        dialogData.Add(43 + 6000, new string[] {
            "교수님 책상을 뒤적거리니까 기분이 이상하네\n\n저를 용서하세요 교수님../1",
            "어딨는거지... 아 이거다!/0",
            "엇 그런데 이 사진은.. 뭐지?\n\n흠 키를 왜 이 아래에?/3",
            "와 교수님 젊었을 땐 안경 쓰셨었구나ㅎㅎ/2",
            "근데 교수님 옆에 계신 분은 누구지?\n\n사이 좋아보이는데~/0",
            "흠 준비는 다 된 것 같으니.../2",
            "이제 가볼까?/0"
        });
        dialogData.Add(50 + 7000, new string[] {
            "[ 정말 떠나시겠습니까? ]\n\n[ 1. 떠난다 2. 더 둘러본다 ]/6"
        });

        // 초상화 데이터
        // 주인공
        portraitData.Add(0, portraitArr[0]); // normal face 디폴트 표정
        portraitData.Add(1, portraitArr[1]); // closed eye 눈 감은 표정
        portraitData.Add(2, portraitArr[2]); // stare face 조금 아련한 표정
        portraitData.Add(3, portraitArr[3]); // side eye 째려보는 표정
        portraitData.Add(4, portraitArr[4]); // little annoying 약간 짜증
        portraitData.Add(5, portraitArr[5]); // very annoying 매우 짜증
        portraitData.Add(6, portraitArr[6]); // 보여지지 않을 초상화
    }

    // 대화 반환 함수
    public string GetDialog(int id, int dialogIndex) // 대사 리턴 (id = 대화상대 객체id , dialogIndex = 대사 인덱스)
    {
        if (!dialogData.ContainsKey(id)) // 퀘스트 진행중 대사가 없을 경우 예외처리
        {
            if (!dialogData.ContainsKey(id - id % 10))
                return GetDialog(id - id % 100, dialogIndex);
            else
                return GetDialog(id - id % 10, dialogIndex);
        }

        if (dialogIndex == dialogData[id].Length) // 대화가 끝나면
            return null; // 종료

        else // 대사가 남아있으면
            return dialogData[id][dialogIndex]; // 대사 반환
    }

    public Sprite GetPortrait(int portraitIndex) // 초상화 이미지 리턴 (portraitIndex == 초상화 번호)
    {
        return portraitData[portraitIndex]; // 초상화 이미지 반환
    }
}
