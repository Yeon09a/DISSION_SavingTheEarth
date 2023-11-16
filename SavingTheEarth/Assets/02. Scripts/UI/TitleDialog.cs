using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleDialog : MonoBehaviour// 타이틀 대화 시스템
{
    public TextMeshProUGUI dialogName; // 대화창 이름
    public TextMeshProUGUI dialogContent; // 대화창 내용
    public Image dialogToggle; // 대화창 토글
    public Transform selectPanel; // 선택지 판넬
    public GameObject[] selectBtns; // 선택지들

    private int eventNum; // 이벤트 번호

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDialogName(string text) // 대화창 이름 설정
    {
        dialogName.text = text;
    }

    public void SetDialogContent(int i, string text) // 대화창 내용 설정
    {
        eventNum = i;
        dialogContent.text = text;
    }

    public void MakeSelect(string[] selects, TitleManager titleManager) // 선택지 만들기
    {
        selectPanel.gameObject.SetActive(true);
        for(int i = 0; i < selects.Length; i++)
        {
            selectBtns[i].SetActive(true);
            selectBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = selects[i];
            string bTag = selectBtns[i].tag;
            selectBtns[i].GetComponent<Button>().onClick.AddListener(() => {titleManager.SetSelectResult(bTag, DeleteSelect);});
        }
    }

    public void DeleteSelect() // 선택지 해제
    {
        for(int i = 0; i < 3; i++)
        {
            selectBtns[i].GetComponent<Button>().onClick.RemoveAllListeners();
            selectBtns[i].SetActive(false);
        }
        selectPanel.gameObject.SetActive(false);
    }
}
