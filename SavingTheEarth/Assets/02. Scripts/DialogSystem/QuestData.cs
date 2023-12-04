using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData // 퀘스트 정보 구성 구조체
{
    public string questName;
    public int[] objId;

    public QuestData(string name, int[] obj) // 생성자
    {
        questName = name;
        objId = obj;
    }
}
