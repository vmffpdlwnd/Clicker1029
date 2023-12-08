using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePopup : MonoBehaviour
{
    private ShopList shopList;

    [SerializeField] private Button CPU;
    [SerializeField] private Button GPU;
    [SerializeField] private Button SKILL;

    [SerializeField]
    private ShopSlot shopSlot;

    List<ShopSlot> CPUSlotList = new List<ShopSlot>();
    List<ShopSlot> GPUSlotList = new List<ShopSlot>();
    List<ShopSlot> SKILLSlotList = new List<ShopSlot>();

    [SerializeField]
    private GameObject CPUPage;
    [SerializeField]
    private GameObject GPUPage;
    [SerializeField]
    private GameObject SKILLPage;

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
        SKILLPage.SetActive(false);

        RefreshData(ShopList.ShopCategory.CPU, CPUSlotList);
    }
    private void OnButtonGpu()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        SKILLPage.SetActive(false);

        RefreshData(ShopList.ShopCategory.GPU, GPUSlotList);
    }
    private void OnButtonSkill()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(true);

        RefreshData(ShopList.ShopCategory.SKILL, SKILLSlotList);
    }
}
