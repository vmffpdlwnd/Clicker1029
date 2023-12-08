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
    private RectTransform CPURect; // 슬록등을 만들어낼 컨텐츠 영역
    [SerializeField]
    private RectTransform GPURect; // 판매리스트를 생성할 영역
    [SerializeField]
    private RectTransform DECORect; // 슬록등을 만들어낼 컨텐츠 영역
    [SerializeField]
    private RectTransform SKILLRect; // 판매리스트를 생성할 영역

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
            shopSlot.InitSlot(this, i); // 슬롯을 생성한 팝업 정보, 몇번째 슬롯인지 index 전달.
            shopSlot.gameObject.name = "BuyeSlot_" + i;
        }
    }

    private void RefreshData()
    {
        // 플레이어 보유금액을 갱신

        // 인벤토리 정보 불러오기
        inventory = GameManager.Instance.INVEN;

        dataList = inventory.GetItemList(); // 아이템정보 가져오고.

        for (int i = 0; i < inventory.MaxSlot; i++)
        {
            if (i < inventory.CurSlot && -1 < inventory.items[i].itemTableID) // 테이블정보가 정상적으로 가지고 있다.
            {
                CPUSlotList[i].RefrshSlot(inventory.items[i]); // 슬롯정보를 갱신  
            }
            else // 아이템이 없는 경우
            {
                CPUSlotList[i].ClearSlot(); // 빈칸 처리
            }
        }
    }

    public void OnButtonCpu()
    {
        RefreshData(); // 목록을 최신화.

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
        RefreshData(); // 목록을 최신화.

        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);
    }
    public void OnButtonDeco()
    {
        RefreshData(); // 목록을 최신화.

        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(true);
        SKILLPage.SetActive(false);
    }
    public void OnButtonSkill()
    {
        RefreshData(); // 목록을 최신화.

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
        RefreshData(); // 팝업창 정보 갱신.
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutElastic);
    }
}
