using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    [SerializeField]
    private Button CPUBtn;
    [SerializeField]
    private Button GPUBtn;
    [SerializeField]
    private Button DECOBtn;
    [SerializeField]
    private Button SKILLBtn;

    private ShopSlot shopSlot;

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
        InitPopup();
    }

    private void InitPopup()
    {
        
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

    private void RefreshData()
    {
        
    }


    private void OnButtonSkill()
    {
        
    }

    private void OnButtonDeco()
    {
        
    }

    private void OnButtonGpu()
    {
        
    }

    private void OnButtonCpu()
    {
        
    }
}
