using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    [SerializeField]
    private ShopList shopList;

    [SerializeField]
    private Button CPUBtn;
    [SerializeField]
    private Button GPUBtn;
    [SerializeField]
    private Button DECOBtn;
    [SerializeField]
    private Button SKILLBtn;

    [SerializeField]
    private ShopSlot shopSlotPrefab;

    List<ShopSlot> CPUSlotList = new List<ShopSlot>();
    List<ShopSlot> GPUSlotList = new List<ShopSlot>();
    List<ShopSlot> DECOSlotList = new List<ShopSlot>();
    List<ShopSlot> SKILLSlotList = new List<ShopSlot>();

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
        if (shopList == null)
        {
            Debug.LogError("ShopList 컴포넌트를 찾을 수 없습니다.");
        }
        else
        {
            StartCoroutine(InitPopup());
        }
    }

    private IEnumerator InitPopup()
    {
        yield return new WaitForSeconds(0.1f);

        ShopList.ShopCategory category = ShopList.ShopCategory.CPU;
        var shopItems = shopList.GetShopItems(category);
        foreach (var item in shopItems)
        {
            Debug.Log("아이템 이름: " + item.ItemName);
            Debug.Log("아이템 가격: " + item.ItemPrice);
        }
    }
    private void Start()
    {
        // 버튼의 클릭 이벤트에 핸들러를 추가
        CPUBtn.onClick.AddListener(OnButtonCpu);
        GPUBtn.onClick.AddListener(OnButtonGpu);
        DECOBtn.onClick.AddListener(OnButtonDeco);
        SKILLBtn.onClick.AddListener(OnButtonSkill);
    }

    private int totalGold;

    public void CalculateGold()
    {
        totalGold = 0;

        if (CPUPage.activeSelf) // CPU창이 활성화 되었을때.
        {
            for (int i = 0; i < CPUSlotList.Count; i++)
            {
                if (CPUSlotList[i].isActiveAndEnabled) // 해당 슬롯이 활성화 된 상태라면.
                {
                    totalGold += CPUSlotList[i].TotalGold;
                }
            }
        }
        if (GPUPage.activeSelf) // GPU창이 활성화 되었을때.
        {
            for (int i = 0; i < 4; i++)
            {
                if (GPUSlotList[i].isActiveAndEnabled)
                {
                    totalGold += GPUSlotList[i].TotalGold;
                }
            }
        }
        if(DECOPage.activeSelf)
        {
            for (int i = 0; i < 4; i++)
            {
                if (DECOSlotList[i].isActiveAndEnabled)
                {
                    totalGold += DECOSlotList[i].TotalGold;
                }
            }
        }
        if (SKILLPage.activeSelf)
        {
            for (int i = 0; i < 4; i++)
            {
                if (SKILLSlotList[i].isActiveAndEnabled)
                {
                    totalGold += SKILLSlotList[i].TotalGold;
                }
            }
        }

    }

    // 팝업에 있는 거래금액을 갱신해주는 함수
    public void RefreshGold()
    {
        CalculateGold();
    }

    private void RefreshData(ShopList.ShopCategory category, List<ShopSlot> slotList)
    {

        var items = shopList.GetShopItems(category);

        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < items.Count)
            {
                slotList[i].RefrshSlot(i);
            }
            else
            {
                slotList[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnButtonCpu()
    {
        CPUPage.SetActive(true);
        GPUPage.SetActive(false);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);

        RefreshData(ShopList.ShopCategory.CPU, CPUSlotList);
    }
    private void OnButtonGpu()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(false);

        RefreshData(ShopList.ShopCategory.GPU, GPUSlotList);
    }
    private void OnButtonDeco()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(true);
        SKILLPage.SetActive(false);

        RefreshData(ShopList.ShopCategory.DECO, DECOSlotList);
    }
    private void OnButtonSkill()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        DECOPage.SetActive(false);
        SKILLPage.SetActive(true);

        RefreshData(ShopList.ShopCategory.SKILL, SKILLSlotList);
    }
}
