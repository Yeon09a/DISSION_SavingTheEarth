using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    public InventoryManager inventoryManager;

    Dictionary<int, QuestData> questList; // 퀘스트 목록 저장

    // Start is called before the first frame update
    void Start()
    {
        questList = new Dictionary<int, QuestData>(); // 초기화
        inventoryManager = GetComponent<InventoryManager>(); // 초기화

        GenerateData();
    }

    void GenerateData() // 퀘스트 데이터 생성
    {
        questList.Add(10, new QuestData("조종실로 가서 조작키 배우기", new int[] { 1000 }));
        questList.Add(20, new QuestData("칠판에서 할 일 체크 후 온실에서 물 주기", new int[] { 2000, 3000 }));
        questList.Add(30, new QuestData("교수님 행방에 의문 갖기&떠날 결심하기", new int[] { 1000 }));
        questList.Add(40, new QuestData("필요한 물품 챙기기", new int[] { 3000, 13000, 4000, 5000, 6000 }));
        questList.Add(50, new QuestData("나갈 결심", new int[] { 7000 }));
        questList.Add(60, new QuestData("BaseMap 퀘스트 클리어", new int[] { 0 }));
    }

    public int GetQuestTalkIndex()
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].objId[questActionIndex])
            questActionIndex++; // 퀘스트 액션 인덱스 증가

        ControlObject(); // 퀘스트 오브젝트 함수

        if (questActionIndex == questList[questId].objId.Length) // 모든 퀘스트 완수 시
            NextQuest(); // 다음 퀘스트로 넘어가기

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true); // 칠판 위에 느낌표 띄우기
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false); // 칠판 위에 느낌표 없애기
                break;
            case 30:
                if (questActionIndex == 1)
                    questObject[1].SetActive(true);
                break;
            case 40:
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                }
                if (questActionIndex == 2)
                {
                    questObject[2].SetActive(false);
                    questObject[3].SetActive(true);
                }
                if (questActionIndex == 3)
                {
                    questObject[3].SetActive(false);
                    questObject[4].SetActive(true);
                    inventoryManager.PutItem(1, 1);// putItem() 호출
                }
                if (questActionIndex == 4)
                {
                    questObject[4].SetActive(false);
                    questObject[5].SetActive(true);
                    inventoryManager.PutItem(2, 1);// putItem() 호출
                }
                if (questActionIndex == 5)
                {
                    questObject[5].SetActive(false);
                }
                break;
        }
    }
}

