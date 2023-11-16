using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemPanel : MonoBehaviour
{
    public Item selectedItem; // æ∆¿Ã≈€
    public ShopManager shopMng;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    private void Start()
    {
        itemImage.sprite = selectedItem.image;
        itemName.text = selectedItem.itName;
        itemPrice.text = selectedItem.price.ToString();

        GetComponent<Button>().onClick.AddListener(ClickShopItemPanel);
    }

    private void ClickShopItemPanel()
    {
        shopMng.selectedItem = selectedItem;
        shopMng.ChangePurchasePanel();
    }
}
