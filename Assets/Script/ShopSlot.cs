using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    private ShopPopup shopPopup;
    private List<ShopList.ShopItem> cpuList;


    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemPriceText;

    [SerializeField]
    private Button purchaseButton;
    private TextMeshProUGUI purchaseButtonText;

    private ShopList shopList;

    private int slotIndex;
    public int SlotIndex { get => slotIndex; }

    private enum PurchaseStatus { NotPurchased, Purchased, Equipped }
    private PurchaseStatus status = PurchaseStatus.NotPurchased;

    private int totalGold;
    public int TotalGold { get => totalGold; }
    private int sellGold;
    public int SellGold { get => sellGold; }

    private void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClick);
        purchaseButtonText = purchaseButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnPurchaseButtonClick()
    {
        if (GameManager.Instance.PlayerCurrentCPU_level < cpuList.Count)  // CPU_level 값이 cpuList의 길이보다 작으면
            

        {
            GameManager.Instance.PlayerCurrentCPU_level++;  // CPU_level 값을 1 증가시킵니다.
        }
        UpdatePurchaseButtonText();
    }

    private void UpdatePurchaseButtonText()
    {
        if (slotIndex < GameManager.Instance.PlayerCurrentCPU_level)  // 현재 슬롯의 인덱스가 CPU_level 값보다 작으면
        {
            purchaseButtonText.text = "장착됨";  // 해당 CPU는 이미 구매하고 장착한 상태입니다.
        }
        else if (slotIndex == GameManager.Instance.PlayerCurrentCPU_level)  // 현재 슬롯의 인덱스가 CPU_level 값과 같으면
        {
            purchaseButtonText.text = "장착";  // 해당 CPU는 구매한 상태이며, 장착할 수 있습니다.
        }
        else
        {
            purchaseButtonText.text = "구매";  // 해당 CPU는 아직 구매하지 않은 상태입니다.
        }
    }
    public void RefrshSlot(int index)
    {
        gameObject.SetActive(true);
        List<ShopList.ShopItem> cpuList = shopList.GetShopItems(ShopList.ShopCategory.CPU);
        if (index < cpuList.Count)
        {
            itemNameText.text = cpuList[index].ItemName;
            // 가격 설정 로직 추가
        }
        else
        {
            itemNameText.text = "준비중";
            // 가격 설정 로직 추가
        }
    }


}