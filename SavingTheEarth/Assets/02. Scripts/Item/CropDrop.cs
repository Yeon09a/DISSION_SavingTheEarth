using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropDrop : MonoBehaviour, IDropHandler
{
    private Transform itemPanelTr; // 아이템 슬롯 transform
    private int cropId = 6;

    void Start()
    {
        itemPanelTr = transform.GetChild(0); // 아이콘이 들어갈 부모 
    }

    public void OnDrop(PointerEventData eventData) // 드롭할 때
    {
        if (Drag.inDrag) // 드래그 하고 있을 때
        {
            int iconId = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.id;

            if (itemPanelTr.childCount == 0 && (iconId == cropId)) // 아이콘이 들어갈 부모가 비어있을 경우(빈 슬롯일 경우) && 슬롯의 타입이 아이템 창이거나 슬롯의 타입과 아이콘의 타입이 같을 경우
            {
                Drag.draggingIcon.transform.SetParent(itemPanelTr);
                Drag.draggingIcon.GetComponent<Drag>().oriParentTr = itemPanelTr;
            }
        }
    }
}
