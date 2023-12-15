using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : Popup_Base, IBaseTownPopup
{
    [Header("Buttons")]
    [SerializeField] private Button CPUBtn;
    [SerializeField] private Button GPUBtn;
    [SerializeField] private Button DECOBtn;
    [SerializeField] private Button SKILLBtn;

    [Header("Shop Slots")]
    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private RectTransform CPURect;
    [SerializeField] private RectTransform GPURect;
    [SerializeField] private RectTransform DECORect;
    [SerializeField] private RectTransform SKILLRect;

    [Header("Pages")]
    [SerializeField] private GameObject CPUPage;
    [SerializeField] private GameObject GPUPage;
    [SerializeField] private GameObject DECOPage;
    [SerializeField] private GameObject SKILLPage;

    private List<List<ShopSlot>> shopSlotLists = new List<List<ShopSlot>>();

    private ShopSlot shopSlot; // ShopSlot 변수 추가

    private void Awake()
    {
        InitPopup();
        SetButtonListeners();
    }

    private void InitPopup()
    {
        // Initialize shop slot lists
        shopSlotLists.Add(new List<ShopSlot>());
        shopSlotLists.Add(new List<ShopSlot>());
        shopSlotLists.Add(new List<ShopSlot>());
        shopSlotLists.Add(new List<ShopSlot>());

        // Create shop slots
        CreateShopSlots(5, CPURect, shopSlotLists[0]);
        CreateShopSlots(1, GPURect, shopSlotLists[1]);
        CreateShopSlots(1, DECORect, shopSlotLists[2]);
        CreateShopSlots(1, SKILLRect, shopSlotLists[3]);
    }

    private void CreateShopSlots(int count, RectTransform parentRect, List<ShopSlot> slotList)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newShopSlotObject = Instantiate(shopSlotPrefab, parentRect);
            shopSlot = newShopSlotObject.GetComponent<ShopSlot>(); // ShopSlot 인스턴스화 및 초기화
            if (shopSlot != null)
            {
                shopSlot.InitSlot(this, i);
                shopSlot.gameObject.name = "BuySlot_" + i;
                slotList.Add(shopSlot);
            }
            else
            {
                Debug.LogError("ShopSlot component not found in the instantiated object.");
            }
        }
    }

    private void SetButtonListeners()
    {
        CPUBtn.onClick.AddListener(OnButtonCpu);
        GPUBtn.onClick.AddListener(OnButtonGpu);
        DECOBtn.onClick.AddListener(OnButtonDeco);
        SKILLBtn.onClick.AddListener(OnButtonSkill);
    }

    private void RefreshData(List<ShopSlot> slotList)
    {
        Inventory inventory = GameManager.Instance.INVEN;

        for (int i = 0; i < inventory.MaxSlot; i++)
        {
            if (i < inventory.CurSlot && inventory.items[i].itemTableID != -1)
            {
                if (i < slotList.Count)
                {
                    slotList[i].RefrshSlot(inventory.items[i]);
                }
            }
            else
            {
                if (i < slotList.Count)
                {
                    slotList[i].ClearSlot();
                }
            }
        }
    }

    public void OnButtonCpu()
    {
        RefreshData(shopSlotLists[0]);
        SetActivePage(CPUPage);
    }

    public void OnButtonGpu()
    {
        RefreshData(shopSlotLists[1]);
        SetActivePage(GPUPage);
    }

    public void OnButtonDeco()
    {
        RefreshData(shopSlotLists[2]);
        SetActivePage(DECOPage);
    }

    public void OnButtonSkill()
    {
        RefreshData(shopSlotLists[3]);
        SetActivePage(SKILLPage);
    }

    private void SetActivePage(GameObject page)
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);

        page.SetActive(true);
    }
   
}
