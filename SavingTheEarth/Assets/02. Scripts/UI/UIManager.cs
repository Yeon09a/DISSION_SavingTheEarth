using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // 인벤토리 창
    public GameObject itemList; // 아이템 창
    public GameObject playerInfoPanel; // 플레이어 정보 창
    public GameObject miniMap; // 미니맵
    public GameObject moneyPanel; // 돈 판넬
    public GameObject timePanel; // 시간 판넬


    public Button inventoryBtn; // 인벤토리(가방) 버튼
    public Toggle itemTgl; // 소지품 토글
    public GameObject itemPanel; // 소지품 판넬
    public GameObject pItemPanel; // 중요 물품 판넬
    public GameObject settingPanel; // 설정 창
    public Button settingBtn; // 설정 창 버튼
    public Button setOkayBtn; // 설정 창 확인 버튼
    public GameObject fullMap; // 전체 맵 창
    public Button mapExitBtn; // 전체 맵 닫기 버튼
    public Button inventoryExitBtn; // 인벤토리 닫기 버튼
    public GameObject trashcan; // 쓰레기통

    public Button goTitleBtn; // 타이틀로 돌아가기 버튼
    public GameObject titleCheckPanel; // 타이틀로 돌아갈 것인지 확인
    public Button titleOkayBtn; // 돌아가기
    public Button titleNoBtn; // 취소

    public GameObject shopPanel; // 상점 판넬
    public Button purchaseBtn; // 구입하기 버튼
    public Button shopExitBtn; // 상점 나가기 버튼

    private bool isInvenOpen = false; // 인벤토리 상태

    public GameObject farmTool; // 농사 도구 UI
    public Image toolImage; // 농사 도구 이미지
    public TextMeshProUGUI toolCount; // 씨앗 개수
    public Sprite[] tools; // 0 : 도구 없음 1 : 호미, 2 : 물뿌리기, 3 : 바구니
    public GameObject farmInfo; // 농사 정보
    public TextMeshProUGUI farmInfoText; // 농사 정보 텍스트

    public GameObject box; // 상자UI


    public Player player; // 플레이어
    public PlayerFarm playerFarm;
    public ShopManager shopMng;

    private float oriPosY;

    
    // Start is called before the first frame update
    void Start()
    {
        player.OpenShop -= OpenShop;
        player.OpenFarmTool -= OpenFarmTool;
        player.CloseFarmTool -= CloseFarmTool;
        player.ChangeFarmTool -= ChangeFarmTool;
        playerFarm.SetSeedCount -= SetToolCount;
        playerFarm.OnFarmInfo -= OnFarmInfo;
        player.OpenBox -= SetBox;

        inventoryBtn.onClick.RemoveAllListeners();
        itemTgl.onValueChanged.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
        setOkayBtn.onClick.RemoveAllListeners();
        mapExitBtn.onClick.RemoveAllListeners();
        goTitleBtn.onClick.RemoveAllListeners();
        titleOkayBtn.onClick.RemoveAllListeners();
        titleNoBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.RemoveAllListeners();
        purchaseBtn.onClick.RemoveAllListeners();
        shopExitBtn.onClick.RemoveAllListeners();

        // UI 액션 연결
        player.OpenShop += OpenShop;
        player.OpenFarmTool += OpenFarmTool;
        player.CloseFarmTool += CloseFarmTool;
        player.ChangeFarmTool += ChangeFarmTool;
        playerFarm.SetSeedCount += SetToolCount;
        playerFarm.OnFarmInfo += OnFarmInfo;
        player.OpenBox += SetBox;

        // UI 리스너 연결
        inventoryBtn.onClick.AddListener(SetInventory);
        itemTgl.onValueChanged.AddListener(OnItemPanel);
        settingBtn.onClick.AddListener(OpenSetting);
        setOkayBtn.onClick.AddListener(CloseSetting);
        mapExitBtn.onClick.AddListener(CloseMap);
        goTitleBtn.onClick.AddListener(OpenCheckTitle);
        titleOkayBtn.onClick.AddListener(ClickTitleOkayBtn);
        titleNoBtn.onClick.AddListener(ClickTitleNoBtn);
        inventoryExitBtn.onClick.AddListener(SetInventory);
        purchaseBtn.onClick.AddListener(PurchaseItem);
        shopExitBtn.onClick.AddListener(CloseShop);

        oriPosY = itemList.GetComponent<RectTransform>().localPosition.y;
    }

    private void SetInventory() // 인벤토리 열기
    {
        if (isInvenOpen) // 인벤토리가 열려있을 때
        {
            inventoryPanel.SetActive(false);
            isInvenOpen = false;
        }
        else // 인벤토리가 닫혀있을 때
        {
            inventoryPanel.SetActive(true);
            isInvenOpen = true;
        }
    }

    private void OnItemPanel(bool itemTglOn) // 소지품, 중요물품 판넬 바꾸기
    {
        if (itemTglOn) // 소지품 토글 체크 시
        {
            itemPanel.SetActive(true);
            pItemPanel.SetActive(false);
        }
        else // 소지품 토글 미체크 시
        {
            itemPanel.SetActive(false);
            pItemPanel.SetActive(true);
        }
    }

    private void OpenSetting() // 설정 창 열기
    {
        settingPanel.SetActive(true);
        // 설정 변수 세팅 추가
    }

    private void CloseSetting() // 설정 창 닫기
    {
        settingPanel.SetActive(false);
        // 설정 변수 세팅 추가
    }

    private void CloseMap() // 지도 닫기
    {
        fullMap.SetActive(false);
    }

    private void OpenCheckTitle() // 타이틀로 돌아갈 것인지 확인
    {
        settingPanel.SetActive(false);
        titleCheckPanel.SetActive(true);
    }

    private void ClickTitleOkayBtn() // 타이틀로 돌아가기 
    {
        GameManager.instance.curMap = MapName.Title;
        SceneLoadingManager.LoadScene("Title");
    }

    private void ClickTitleNoBtn() // 타이틀로 돌아가지 않기
    {
        settingPanel.SetActive(true);
        titleCheckPanel.SetActive(false);
    }

    public void OpenShop() // 상점 열기
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop() // 상점 닫기
    {
        shopPanel.SetActive(false);
    }

    public void PurchaseItem() // 구매하기
    {
        shopMng.Purchase();
    }

    private void OpenFarmTool() // 농사 도구 UI 열기
    {
        farmTool.SetActive(true);
    }

    private void CloseFarmTool() // 농사 도구 UI 닫기
    {
        toolImage.sprite = tools[0];
        farmTool.SetActive(false);
    }

    private void ChangeFarmTool(int toolNum) // 농사 UI 바꾸기
    {
        toolImage.sprite = tools[toolNum];
        if (toolNum != 3)
        {
            toolCount.gameObject.SetActive(false);
        } else
        {
            toolCount.gameObject.SetActive(true);
            toolCount.text = (DataManager.instance.nowPlayerData.haveItems.ContainsKey(6) ? DataManager.instance.nowPlayerData.haveItems[6].count : 0).ToString();
            playerFarm.seedCount = int.Parse(toolCount.text);
        }
    }

    public void SetToolCount(int count) // 농사 UI 개수 표시
    {
        toolCount.text = count.ToString();
    }

    public void OnFarmInfo(string infoStr) // 농사 Info 표시
    {
        farmInfo.SetActive(false);
        farmInfo.SetActive(true);
        farmInfoText.text = infoStr;
    }

    private void SetBox() // 박스 열기
    {
        inventoryPanel.GetComponent<RectTransform>().localPosition = new Vector3(324f, 56f, 0f);
        itemList.GetComponent<RectTransform>().localPosition = new Vector3(324f, oriPosY + 56, 0f);
        isInvenOpen = true;
        inventoryExitBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.AddListener(CloseBox);
        trashcan.SetActive(false);
        inventoryPanel.SetActive(true);
        box.SetActive(true);
    }

    private void CloseBox() // 박스 닫기
    {
        inventoryPanel.GetComponent<RectTransform>().localPosition = new Vector3(-19f, 0, 0);
        itemList.GetComponent<RectTransform>().localPosition = new Vector3(-19f, oriPosY, 0f);
        isInvenOpen = true;
        inventoryExitBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.AddListener(SetInventory);
        trashcan.SetActive(false);
        inventoryPanel.SetActive(false);
        box.SetActive(false);
    }
}
