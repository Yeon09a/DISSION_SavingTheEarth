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
        canvasTr = GameObject.FindWithTag("Canvas").transform;
        itemInfoTr = canvasTr.GetChild(canvasTr.childCount - 1).transform;
        GetComponent<Image>().sprite = itemInfo.image;
    }

    private void Update()
    {
        if (isOpened) // 타이머 시작
        {
            curTime -= Time.deltaTime;
            if(curTime < 0) // 2초 지나면
            {
                // 판넬 활성화
                itemInfoTr.GetChild(1).GetComponent<Image>().sprite = itemInfo.image;
                itemInfoTr.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemInfo.itName;
                itemInfoTr.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemInfo.info;
                itemInfoTr.GetComponent<RectTransform>().position = transform.GetChild(0).position + new Vector3(-180, 90, 0);
                itemInfoTr.gameObject.SetActive(true);
                curTime = 2.0f;
                isOpened = false; // 타이머가 끝났으므로 false
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpened = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpened = false;
        itemInfoTr.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
