using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    private ShopPopup shopPopup;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemPriceText;

    [SerializeField]
    private Button purchaseButton;
    private TextMeshProUGUI purchaseButtonText;

    private InventoryItemData data; // 해당 슬롯 아이템 데이터.

    private int slotIndex;
    public int SlotIndex { get => slotIndex; }

    private enum PurchaseStatus { NotPurchased, Purchased, Equipped }
    private PurchaseStatus status = PurchaseStatus.NotPurchased;
    
    public int TotalGold { get => totalGold; }
    private int totalGold;
   
    public int SellGold { get => sellGold; }
    private int sellGold;

    private int curCount;
    private int itemID;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClick);
        purchaseButtonText = purchaseButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnPurchaseButtonClick()
    {
        // 'GameManager'의 'shopTable'에서 아이템의 'uid'를 사용하여 아이템 정보를 찾습니다.
        if (GameManager.Instance.shopTable.TryGetValue(data.itemTableID, out object itemObject))
        {
            ClickerGame.Param itemData = itemObject as ClickerGame.Param;
            if (itemData != null)
            {
                // 아이템의 정보를 얻었습니다.
                // 'itemData'를 이용하여 아이템의 가격 정보를 얻을 수 있습니다.
                int itemPrice = itemData.Gold;

                // 아이템을 구매합니다.
                // 인벤토리에 충분한 공간과 사용자의 골드가 충분한지 확인해야 합니다.
                if (GameManager.Instance.PlayerGold >= itemPrice && !GameManager.Instance.INVEN.isFull())
                {
                    // 사용자의 골드를 차감합니다.
                    GameManager.Instance.PlayerGold -= itemPrice;

                    // 인벤토리에 아이템을 추가합니다.
                    GameManager.Instance.INVEN.AddItem(data);

                    // 아이템의 상태를 '구매됨'으로 변경합니다.
                    status = PurchaseStatus.Purchased;

                    // 구매 버튼의 텍스트를 '장착'으로 변경합니다.
                    purchaseButtonText.text = "장착";
                }
                else
                {
                    // 사용자에게 골드가 부족하거나 인벤토리 공간이 부족한 것을 알립니다.
                    Debug.Log("골드가 부족하거나 인벤토리 공간이 부족합니다.");
                }
            }
            else
            {
                // 'itemObject'를 'ClickerGame.Param' 타입으로 변환하지 못했습니다.
                // 이 경우에 적절한 에러 처리를 수행하시면 됩니다.
            }
        }
        else
        {
            // 아이템의 정보를 얻지 못했습니다.
            // 이 경우에 적절한 에러 처리를 수행하시면 됩니다.
        }


    }

    public void ClearSlot()
    {
        gameObject.SetActive(false);
    }

    public void RefrshSlot(InventoryItemData item)
    {
        gameObject.SetActive(true);
        itemID = item.itemTableID;
        curCount = 0;
    }


    public void InitSlot(ShopPopup shopPopup, int index)
    {
        this.shopPopup = shopPopup;
        slotIndex = index;

    }
}