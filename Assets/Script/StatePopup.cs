using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePopup : MonoBehaviour
{
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

    // ClickerGame 객체를 참조할 필드를 추가합니다.
    [SerializeField]
    private ClickerGame clickerGame;

    private void Start()
    {
        // 각 버튼에 대해 클릭 이벤트 리스너를 추가합니다.
        CPU.onClick.AddListener(OnButtonCpu);
        GPU.onClick.AddListener(OnButtonGpu);
        SKILL.onClick.AddListener(OnButtonSkill);
    }

    private void Awake()
    {
        clickerGame = FindObjectOfType<ClickerGame>();
        if (clickerGame == null)
        {
            Debug.LogError("ClickerGame object not found!");
        }
    }


    private void RefreshData(int categoryIndex, List<ShopSlot> slotList)
    {
        var items = clickerGame.sheets[categoryIndex].list;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < items.Count)
            {
                // 아이템의 정보를 이용해서 InventoryItemData 객체를 생성합니다.
                InventoryItemData itemData = new InventoryItemData();
                itemData.itemTableID = items[i].uid;
                // 생성한 객체를 RefrshSlot 메소드에 전달합니다.
                slotList[i].RefrshSlot(itemData);
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

        RefreshData(0, CPUSlotList);  // CPU 카테고리는 index 0으로 가정
    }
    private void OnButtonGpu()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        SKILLPage.SetActive(false);

        RefreshData(1, GPUSlotList);  // GPU 카테고리는 index 1으로 가정
    }
    private void OnButtonSkill()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(true);

        RefreshData(2, SKILLSlotList);  // SKILL 카테고리는 index 2으로 가정
    }
}
