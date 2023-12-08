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

    private InventoryItemData data; // �ش� ���� ������ ������.

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
        // 'GameManager'�� 'shopTable'���� �������� 'uid'�� ����Ͽ� ������ ������ ã���ϴ�.
        if (GameManager.Instance.shopTable.TryGetValue(data.itemTableID, out object itemObject))
        {
            ClickerGame.Param itemData = itemObject as ClickerGame.Param;
            if (itemData != null)
            {
                // �������� ������ ������ϴ�.
                // 'itemData'�� �̿��Ͽ� �������� ���� ������ ���� �� �ֽ��ϴ�.
                int itemPrice = itemData.Gold;

                // �������� �����մϴ�.
                // �κ��丮�� ����� ������ ������� ��尡 ������� Ȯ���ؾ� �մϴ�.
                if (GameManager.Instance.PlayerGold >= itemPrice && !GameManager.Instance.INVEN.isFull())
                {
                    // ������� ��带 �����մϴ�.
                    GameManager.Instance.PlayerGold -= itemPrice;

                    // �κ��丮�� �������� �߰��մϴ�.
                    GameManager.Instance.INVEN.AddItem(data);

                    // �������� ���¸� '���ŵ�'���� �����մϴ�.
                    status = PurchaseStatus.Purchased;

                    // ���� ��ư�� �ؽ�Ʈ�� '����'���� �����մϴ�.
                    purchaseButtonText.text = "����";
                }
                else
                {
                    // ����ڿ��� ��尡 �����ϰų� �κ��丮 ������ ������ ���� �˸��ϴ�.
                    Debug.Log("��尡 �����ϰų� �κ��丮 ������ �����մϴ�.");
                }
            }
            else
            {
                // 'itemObject'�� 'ClickerGame.Param' Ÿ������ ��ȯ���� ���߽��ϴ�.
                // �� ��쿡 ������ ���� ó���� �����Ͻø� �˴ϴ�.
            }
        }
        else
        {
            // �������� ������ ���� ���߽��ϴ�.
            // �� ��쿡 ������ ���� ó���� �����Ͻø� �˴ϴ�.
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