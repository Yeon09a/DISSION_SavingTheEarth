using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HaveItemInfo
{
    public int place; // 아이템 위치
    public int count; // 아이템 개수
    public int slotNum; // 아이템이 들어가있는 슬롯 인덱스

    public HaveItemInfo(int place, int count, int slotNum) // 생성자
    {
        this.place = place; // 분류(0(아이템 창), 1(소지품), 2(중요물품))
        this.count = count;
        this.slotNum = slotNum;
    }
}

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemListSlots; // 하단 아이템 창 슬롯 배열
    public GameObject[] itemSlots; // 소지품 슬롯 배열
    public GameObject[] pItemSlots; // 중요 물품 슬롯 배열

    private bool[] checkItemList; // 아이템 창 슬롯이 비어있는지 체크
    private bool[] checkItem; // 소지품 슬롯이 비어있는지 체크
    private bool[] checkPItem; // 중요 물품 슬롯이 비어있는지 체크

    public GameObject itemIcon; // 아이템 아이콘 프리팹

    public ItemDic items; // 아이템 종류 에셋

    public SlotClick[] slots; // 슬롯 배열
    private SlotClick selectedSlot = null; // 선택된 슬롯

    void Update()
    {
        // 키보드 입력을 통해 슬롯 선택
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // 변경된 부분: 슬롯 번호를 SlotClick 객체로 변환
                SlotClick clickedSlot = slots[i];
                HandleSlotSelection(clickedSlot);
            }
        }
    }

    public void HandleSlotSelection(SlotClick clickedSlot)
    {
        bool isSelected = clickedSlot.IsSelected();

        if (isSelected)
        {
            // 이미 선택된 슬롯을 다시 선택한 경우
            clickedSlot.Deselect();
            selectedSlot = null; // 선택된 슬롯 업데이트
        }
        else
        {
            // 새로운 슬롯을 선택한 경우
            if (selectedSlot != null)
            {
                // 이전에 선택한 슬롯이 있으면 해당 슬롯을 선택 해제
                selectedSlot.Deselect();
            }

            clickedSlot.ToggleSelection();
            selectedSlot = clickedSlot; // 선택된 슬롯 업데이트

            // EventSystem을 사용하여 현재 선택된 게임 오브젝트 변경
            EventSystem.current.SetSelectedGameObject(clickedSlot.gameObject);
        }
    }

    public void SelectSlot(SlotClick clickedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ToggleSelection(); // 이전에 선택한 슬롯 선택 해제
        }

        selectedSlot = clickedSlot; // 새로운 슬롯 선택
        selectedSlot.ToggleSelection();
    }

    private void Awake()
    {
        // 나중에 저장 데이터 필요
        checkItemList = new bool[5];
        checkItem = new bool[15];
        checkPItem = new bool[15];
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateInventory();
    }



    public void UpdateInventory()
    {
        // slots 배열 초기화
        slots = GetComponentsInChildren<SlotClick>();

        // 선택된 슬롯 초기화
        selectedSlot = null;

        // 소지한 아이템 불러오기
        foreach (KeyValuePair<int, HaveItemInfo> item in DataManager.instance.nowPlayerData.haveItems)
        {
            if (item.Value.place == 0) // 아이템 창
            {
                LoadItemIcon(itemListSlots, checkItemList, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 1) // 소지품
            {
                LoadItemIcon(itemSlots, checkItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else // 중요물품
            {
                LoadItemIcon(pItemSlots, checkPItem, item.Key, item.Value.slotNum, item.Value.count);
            }
        }
    }

    private void LoadItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int count) // 아이템 아이콘 불러오기
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon 속성 설정(itemicon으로 이동)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true;

        icon.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    private int CheckHaveItem(int id) // 아이템을 현재 가지고 있는지 확인
    {
        if (DataManager.instance.nowPlayerData.haveItems.ContainsKey(id)) // 아이템을 가지고 있는 경우
        {
            return DataManager.instance.nowPlayerData.haveItems[id].place;
        }
        else // 가지고 있지 않은 경우
        {
            return -1;
        }
    }

    private int CheckItemList() // 아이템 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkItemList.Length; i++)
        {
            if(checkItemList[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckItemSlots() // 소지품 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkItem.Length; i++) 
        {
            if (checkItem[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckPItemSlots() // 중요물품 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkPItem.Length; i++)
        {
            if (checkPItem[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private bool PutNewItem(int id) // 새로 얻은 아이템 획득
    {
        int lIndex = CheckItemList();

        if (lIndex == -1) // 아이템 창에 빈 슬롯이 없는 경우
        {
            if (items.items[id].type == 1) // 아이템 종류가 소지품
            {
                int iIndex = CheckItemSlots();
                if (iIndex != -1) // 소지품 창에 빈 슬롯이 있는 경우
                {
                    CreateItemIcon(itemSlots, checkItem, id, iIndex, 1);
                    return true;
                }
                else // 빈 슬롯이 없는 경우
                {
                    return false;
                }
            }
            else // 아이템 종류가 중요 물품
            {
                int pIndex = CheckPItemSlots();
                if (pIndex != -1) // 중요 물품 창에 빈 슬롯이 있는 경우
                {
                    CreateItemIcon(pItemSlots, checkPItem, id, pIndex, 2);
                    return true;
                }
                else // 빈 슬롯이 없는 경우
                {
                    return false;
                }
            }
        }
        else // 아이템 창에 빈 슬롯이 있는 경우
        {
            // 아이템 리스트에 아이템 넣음
            CreateItemIcon(itemListSlots, checkItemList, id, lIndex, 0);
            return true;
        }
    }
    
    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int place) // 아이템 아이콘 생성
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon 속성 설정(itemicon으로 이동)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true ;
        DataManager.instance.nowPlayerData.haveItems.Add(id, new HaveItemInfo(place, 1, index));
    }

    public void PutHaveItem(int id, GameObject[] slots) // 기존에 가지고 있던 아이템 추가
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += 1;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }

    public bool PutItem(int id) // 아이템 획득
    {
        int place = CheckHaveItem(id);
        if(place == -1) // 새로 얻은 경우
        {
            return PutNewItem(id);
        } 
        else if(place == 0) // 해당 아이템이 아이템 창에 있는 경우
        {
            PutHaveItem(id, itemListSlots);
            return true;
        } 
        else if(place == 1) // 소지품 창에 있는 경우
        {
            PutHaveItem(id, itemSlots);
            return true;
        } 
        else if(place == 2) // 중요물품 창에 있는 경우
        {
            PutHaveItem(id, pItemSlots);
            return true;
        }
        else
        {
            return false;
        }
    }
}
