using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClick : MonoBehaviour, IPointerClickHandler
{
    public int slotNum; // 슬롯 번호
    private bool isSelected = false; // 슬롯 선택 여부
    private Color originalColor; // 원래 색상

    public Color selectedColor; // 선택된 색상

    // 이전에 선택한 슬롯을 추적하기 위한 변수
    private static SlotClick previousSelectedSlot;

    void Start()
    {
        originalColor = GetComponent<Image>().color; // 슬롯의 원래 색상 저장
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelection();
    }

    public void ToggleSelection()
    {
        isSelected = !isSelected;

        // 이전에 선택한 슬롯의 선택 해제
        if (previousSelectedSlot != null && previousSelectedSlot != this)
        {
            previousSelectedSlot.Deselect();
        }

        if (isSelected)
        {
            Color blackColor = Color.black;
            GetComponent<Image>().color = blackColor; // 선택된 슬롯의 색상 변경
        }
        else
        {
            GetComponent<Image>().color = originalColor; // 선택 해제된 슬롯의 색상 원래대로
        }

        // 이전에 선택한 슬롯 업데이트
        previousSelectedSlot = isSelected ? this : null;
    }

    public void Deselect()
    {
        isSelected = false;
        GetComponent<Image>().color = originalColor; // 선택 해제된 슬롯의 색상 원래대로
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
