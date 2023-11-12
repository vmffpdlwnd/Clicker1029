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
        if (GameManager.Instance.PlayerCurrentCPU_level < cpuList.Count)  // CPU_level ���� cpuList�� ���̺��� ������
            

        {
            GameManager.Instance.PlayerCurrentCPU_level++;  // CPU_level ���� 1 ������ŵ�ϴ�.
        }
        UpdatePurchaseButtonText();
    }

    private void UpdatePurchaseButtonText()
    {
        if (slotIndex < GameManager.Instance.PlayerCurrentCPU_level)  // ���� ������ �ε����� CPU_level ������ ������
        {
            purchaseButtonText.text = "������";  // �ش� CPU�� �̹� �����ϰ� ������ �����Դϴ�.
        }
        else if (slotIndex == GameManager.Instance.PlayerCurrentCPU_level)  // ���� ������ �ε����� CPU_level ���� ������
        {
            purchaseButtonText.text = "����";  // �ش� CPU�� ������ �����̸�, ������ �� �ֽ��ϴ�.
        }
        else
        {
            purchaseButtonText.text = "����";  // �ش� CPU�� ���� �������� ���� �����Դϴ�.
        }
    }
    public void RefrshSlot(int index)
    {
        gameObject.SetActive(true);
        List<ShopList.ShopItem> cpuList = shopList.GetShopItems(ShopList.ShopCategory.CPU);
        if (index < cpuList.Count)
        {
            itemNameText.text = cpuList[index].ItemName;
            // ���� ���� ���� �߰�
        }
        else
        {
            itemNameText.text = "�غ���";
            // ���� ���� ���� �߰�
        }
    }


}