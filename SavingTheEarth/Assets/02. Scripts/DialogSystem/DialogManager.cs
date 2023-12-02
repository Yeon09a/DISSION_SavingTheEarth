using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject scanObject; // 레이 충돌 오브젝트

    public TextMeshProUGUI dialogText; // 대화 텍스트
    public GameObject dialogBox; // 대화창
    public Image portrait; // 캐릭터 초상화

    public bool isTalk; // 대화 여부
    public ConversationManager convoManager; // ConversationManager : 대사 생성 매니저
    public int dialogIndex;

    private void Start()
    {
  
    }

    public void Talk(GameObject scanObj) // 대화 시작시 호출될 함수 => 플레이어 스크립트에서 호출 필요!!
    {
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Conversation(objData.id, objData.isNPC);

        dialogBox.SetActive(isTalk); // 대화창 팝업
    }

    void Conversation(int id, bool isNPC)
    {
        string dialogData = convoManager.GetDialog(id, dialogIndex); // 대사 가져와서 저장

        if (dialogData == null) // 대화가 종료됐을 때
        {
            isTalk = false;
            dialogIndex = 0; // 대사 인덱스 초기화
            return; // 종료
        }

        if (isNPC) // 대화상대가 NPC라면
        {
            dialogText.text = dialogData.Split('/')[0]; // 대화창 텍스트로 띄우기 (Split()으로 구분자 제외)

            //portrait.sprite = convoManager.GetPortrait(id, int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환
            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환 

            portrait.color = new Color(1, 1, 1, 1); // NPC일 때만 초상화 띄우기
        }
        else // 대화상대가 아이템이라면 (독백)
        {
            dialogText.text = dialogData.Split('/')[0]; // 대화창 텍스트로 띄우기 (Split()으로 구분자 제외)

            //portrait.sprite = convoManager.GetPortrait(id, int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환
            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환

            if(int.Parse(dialogData.Split('/')[1]) == 6) // 초상화 필요 없는 대화일 경우
                portrait.color = new Color(1, 1, 1, 0); // 초상화 안보이게
            else
                portrait.color = new Color(1, 1, 1, 1); 
        }

        isTalk = true;
        dialogIndex++; // 다음 대사로 넘어가기
    }
}
