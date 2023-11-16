using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int money; // 돈
    private int count; // 아이템 개수
    private int price; // 결제값

    public GameObject purchasePanel; // 구매 판넬
    public TextMeshProUGUI dialogText; // 선장 대화
    public Toggle purchaseItemTgl; // 아이템/무기 토글
    public GameObject itemPanel; // 아이템 판넬
    public GameObject weaponPanel; // 무기 판넬
    public RectTransform itemcontent; // 아이템 스크롤뷰 content
    public RectTransform weaponContent; // 무기 스크롤뷰 content
    public GameObject countPanel; // 개수 판넬
    public Button pBtn; // 개수 추가 버튼
    public Button mBtn; // 개수 감소 버튼
    public TextMeshProUGUI countText; // 구매 개수 텍스트
    public TextMeshProUGUI purchaseBtnText; // 구매 버튼 텍스트
    public TextMeshProUGUI moneyText; // 돈 텍스트
    public TextMeshProUGUI priceText; // 가격 텍스트
    public GameObject cantPurchasePanel; // 구매 안내 판넬
    public Button cantOkayBtn; // 확인 버튼

    public Item selectedItem; // 선택된 아이템

    private string[] dialogArr = {"마음에 드는 게 있으면 골라보라고!", "구매해줘서 고마워!"};

    public InventoryManager invenMng; 


    // Start is called before the first frame update
    void Start()
    {
        //money = DataManager.instance.nowPlayerData.money;
        moneyText.text = money.ToString();
        priceText.text = "0";
        count = 1;
        dialogText.text = dialogArr[0];

        // 스크롤뷰 content 높이 세팅
        int itemCount = itemcontent.transform.childCount;
        itemcontent.sizeDelta = new Vector2(itemcontent.sizeDelta.x, 150 * (itemCount / 2) + 10 * (itemCount / 2) + (itemCount % 2 == 1 ? 150 : -10));
        itemCount = weaponContent.transform.childCount;
        weaponContent.sizeDelta = new Vector2(itemcontent.sizeDelta.x, 150 * (itemCount / 2) + 10 * (itemCount / 2) + (itemCount % 2 == 1 ? 150 : -10));

        // UI 이벤트 추가
        purchaseItemTgl.onValueChanged.AddListener(OnWeaponPanel);
        pBtn.onClick.AddListener(AddCount);
        mBtn.onClick.AddListener(MinusCount);
        cantOkayBtn.onClick.AddListener(ClickCantOkayBtn);
    }

    private void OnWeaponPanel(bool WeaponTglOn) // 아이템, 무기 판넬 바꾸기
    {
        if (WeaponTglOn) // 무기 토글 체크 시
        {
            itemPanel.SetActive(false);
            weaponPanel.SetActive(true);
        }
        else // 무기 토글 미체크 시
        {
            itemPanel.SetActive(true);
            weaponPanel.SetActive(false);
        }
    }

    public void ChangePurchasePanel() // 구매 판넬 업데이트
    {
        purchasePanel.SetActive(true);
        count = 1;
        countText.text = count.ToString();
        purchasePanel.transform.GetChild(0).GetComponent<Image>().sprite = selectedItem.image;
        purchasePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedItem.price.ToString();
        purchasePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = selectedItem.info;

        if(selectedItem.id != 1) // 아이템이 창이 아니면
        {
            countPanel.SetActive(true);
            purchaseBtnText.text = "구입하기";
        } 
        else // 아이템이 창이면
        {
            countPanel.SetActive(false);
            purchaseBtnText.text = "업그레이드";
        }
    }

    private void AddCount() // 구매 개수 추가
    {
        if(count < 100)
        {
            count++;
            countText.text = count.ToString();
            price = selectedItem.price * count;
            priceText.text = price.ToString();
        }
    }

    private void MinusCount() // 구매 개수 감소
    {
        if(count > 1)
        {
            count--;
            countText.text = count.ToString();
            price = selectedItem.price * count;
            priceText.text = price.ToString();
        }
    }

    public void OpenCountPanel() // 개수 판넬 열기
    {
        countPanel.SetActive(true);
    }

    public void CloseCountPanel() // 개수 판넬 닫기
    {
        countPanel.SetActive(false);
    }

    public void Purchase() // 구매
    {
        if (money >= price)
        {
            bool isNotFull = invenMng.PutItem(selectedItem.id);
            if (isNotFull)
            {
                money -= price;
                DataManager.instance.nowPlayerData.money = money;
                moneyText.text = money.ToString();
                dialogText.text = dialogArr[1];
                //invenMng.UpdateInventory();
            } else
            {
                cantPurchasePanel.SetActive(true);
                cantPurchasePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "가방에 빈 자리가 없어/n구매할 수 없습니다.";
            }

        } else
        {
            cantPurchasePanel.SetActive(true);
            cantPurchasePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "코인이 부족하여/n구매할 수 없습니다. ";
        }

    }

    public void ClickCantOkayBtn() // 안내 확인 버튼
    {
        cantPurchasePanel.SetActive(false);
    }
}
