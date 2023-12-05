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

    public GameObject toggle; // 토글

    public bool isTalk; // 대화 여부
    public ConversationManager convoManager; // ConversationManager : 대사 생성 매니저
    public int dialogIndex;

    public QuestManager questManager; // 퀘스트 생성 매니저

    private void Start()
    {
        StartCoroutine(GameStartDialog());

        Debug.Log(questManager.CheckQuest());
    }

    IEnumerator GameStartDialog()
    {
        dialogBox.SetActive(true);
        toggle.SetActive(false); // 토글 안보이게 처리
        portrait.gameObject.SetActive(true);
        dialogText.text = "교수님과 통신 연결이 끊긴 지 벌써 일주일 째...\n\n" +
            "해저 nnnnkm 연구용 잠수함에 혼자 남겨졌다.";

        yield return new WaitForSeconds(4f);

        portrait.gameObject.SetActive(true);
        dialogText.text = "세상이 망해도 하루는 어김없이 시작되는구나\n\n오늘도 일하러 가보실까?";

        yield return new WaitForSeconds(3f);

        dialogText.text = "일단 조종실로 가서 조종대부터 체크하자";

        yield return new WaitForSeconds(2.5f);

        dialogBox.SetActive(false);
    }

    public void Talk(GameObject scanObj) // 대화 시작시 호출될 함수
    {
        GameManager.instance.isTalk = true;

        scanObject = scanObj;

        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Conversation(objData.id, objData.isNPC);
        dialogBox.SetActive(isTalk); // 대화창 팝업
        toggle.SetActive(true); // 토글 다시 보이게 처리
    }

    void Conversation(int id, bool isNPC)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(); // 퀘스트 대사 인덱스 불러오기 (return == questId + questActionIndex)
        string dialogData = convoManager.GetDialog(id + questTalkIndex, dialogIndex); // 대사 가져와서 저장 ( id == objData.Id )

        if (dialogData == null) // 대화가 종료됐을 때
        {
            isTalk = false;
            dialogIndex = 0; // 대사 인덱스 초기화
            Debug.Log(questManager.CheckQuest(id)); // 다음 퀘스트로 넘어가기

            GameManager.instance.isTalk = false;
            return; // 종료
        }

        if (isNPC) // 대화상대가 NPC라면
        {
            dialogText.text = dialogData.Split('/')[0]; // 대화창 텍스트로 띄우기 (Split()으로 구분자 제외)

            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환 

            portrait.gameObject.SetActive(true);
        }
        else // 대화상대가 아이템이라면 (독백)
        {
            dialogText.text = dialogData.Split('/')[0]; // 대화창 텍스트로 띄우기 (Split()으로 구분자 제외)

            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // 초상화 이미지 변환

            if (int.Parse(dialogData.Split('/')[1]) == 6) // 초상화 필요 없는 대화일 경우
                portrait.gameObject.SetActive(false); // 초상화 안보이게 (투명하게 처리)
            else
                portrait.gameObject.SetActive(true);
        }

        isTalk = true;
        dialogIndex++; // 다음 대사로 넘어가기
    }

    // 토글 버튼 함수
    public void ToggleClick()
    {
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Conversation(objData.id, objData.isNPC);

        dialogBox.SetActive(isTalk); // 대화창 팝업
    }
}
