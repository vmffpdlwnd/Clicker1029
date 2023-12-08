using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopPopup : Popup_Base, IBaseTownPopup
{
    [SerializeField]
    private Button CPUBtn;
    [SerializeField]
    private Button GPUBtn;
    [SerializeField]
    private Button DECOBtn;
    [SerializeField]
    private Button SKILLBtn;

    [SerializeField]
    private GameObject shopSlotPrefab;
    [SerializeField]
    private RectTransform CPURect; // ���ϵ��� ���� ������ ����
    [SerializeField]
    private RectTransform GPURect; // �ǸŸ���Ʈ�� ������ ����
    [SerializeField]
    private RectTransform DECORect; // ���ϵ��� ���� ������ ����
    [SerializeField]
    private RectTransform SKILLRect; // �ǸŸ���Ʈ�� ������ ����

    private ShopSlot shopSlot;
    private Inventory inventory;

    List<ShopSlot> CPUSlotList = new List<ShopSlot>();
    List<ShopSlot> GPUSlotList = new List<ShopSlot>();
    List<ShopSlot> DECOSlotList = new List<ShopSlot>();
    List<ShopSlot> SKILLSlotList = new List<ShopSlot>();
    List<InventoryItemData> dataList;

    [SerializeField]
    private GameObject CPUPage;
    [SerializeField]
    private GameObject GPUPage;
    [SerializeField]
    private GameObject DECOPage;
    [SerializeField]
    private GameObject SKILLPage;

    private void Awake()
    {
        InitPopup();
        OnButtonCpu();
        OnButtonGpu();
        OnButtonSkill();
        OnButtonDeco();
        PopupClose();
    }

    private void InitPopup()
    {

        for (int i = 0; i < 5; i++) // Pentium Silver, Pentium Gold, i3, I5, I7
        {
            shopSlot = Instantiate(shopSlotPrefab, CPURect).GetComponent<ShopSlot>();
            shopSlot.InitSlot(this, i); // ������ ������ �˾� ����, ���° �������� index ����.
            shopSlot.gameObject.name = "BuyeSlot_" + i;
        }
    }

    private void RefreshData()
    {
        // �÷��̾� �����ݾ��� ����

        // �κ��丮 ���� �ҷ�����
        inventory = GameManager.Instance.INVEN;

        dataList = inventory.GetItemList(); // ���������� ��������.

        for (int i = 0; i < inventory.MaxSlot; i++)
        {
            if (i < inventory.CurSlot && -1 < inventory.items[i].itemTableID) // ���̺������� ���������� ������ �ִ�.
            {
                CPUSlotList[i].RefrshSlot(inventory.items[i]); // ���������� ����  
            }
            else // �������� ���� ���
            {
                CPUSlotList[i].ClearSlot(); // ��ĭ ó��
            }
        }
    }

    public void OnButtonCpu()
    {
        RefreshData(); // ����� �ֽ�ȭ.

        CPUPage.SetActive(true);
        GPUPage.SetActive(false);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);

        CPUPage.SetActive(true);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(false);
    }
    public void OnButtonGpu()
    {
        RefreshData(); // ����� �ֽ�ȭ.

        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);
    }
    public void OnButtonDeco()
    {
        RefreshData(); // ����� �ֽ�ȭ.

        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(true);
        SKILLPage.SetActive(false);
    }
    public void OnButtonSkill()
    {
        RefreshData(); // ����� �ֽ�ȭ.

        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(true);
    }

    public void PopupClose()
    {
        transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutElastic);
    }

    public void PopupOpen()
    {
        RefreshData(); // �˾�â ���� ����.
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutElastic);
    }
}
