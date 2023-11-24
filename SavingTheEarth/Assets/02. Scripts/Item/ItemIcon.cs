using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemInfo; // 아이템 정보

    private Transform itemInfoTr; // 아이템 정보 transform
    private Transform canvasTr; // Canvas transform

    private float curTime = 2.0f; // 마우스가 머무르는 시간

    private bool isOpened = false; // 2초 타이머 시작

    private void Start()
    {
        // Canvas와 아이템 정보 transform 초기화
        canvasTr = GameObject.FindWithTag("Canvas").transform;
        itemInfoTr = canvasTr.GetChild(canvasTr.childCount - 1).transform;
        GetComponent<Image>().sprite = itemInfo.image; // 아이템 아이콘에 아이템 이미지 설정
    }

    private void Update()
    {
        if (isOpened) // 마우스가 머무르는 시간 카운트 시작
        {
            curTime -= Time.deltaTime;
            if (curTime < 0) // 2초가 지나면
            {
                // 판넬 활성화 및 아이템 정보 설정
                itemInfoTr.GetChild(1).GetComponent<Image>().sprite = itemInfo.image;
                itemInfoTr.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemInfo.itName;
                itemInfoTr.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemInfo.info;
                itemInfoTr.GetComponent<RectTransform>().position = transform.GetChild(0).position + new Vector3(-180, 90, 0);
                itemInfoTr.gameObject.SetActive(true);
                curTime = 2.0f; // 타이머 초기화
                isOpened = false; // 타이머가 끝났으므로 false로 설정
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpened = true; // 마우스가 아이콘에 들어왔을 때 타이머 시작
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpened = false; // 마우스가 아이콘에서 나갔을 때 타이머 중지
        itemInfoTr.gameObject.SetActive(false); // 아이템 정보 판넬 비활성화
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets(); // 사용하지 않는 에셋 언로드
    }
}
