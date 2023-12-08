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

    // ClickerGame ��ü�� ������ �ʵ带 �߰��մϴ�.
    [SerializeField]
    private ClickerGame clickerGame;

    private void Start()
    {
        // �� ��ư�� ���� Ŭ�� �̺�Ʈ �����ʸ� �߰��մϴ�.
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
                // �������� ������ �̿��ؼ� InventoryItemData ��ü�� �����մϴ�.
                InventoryItemData itemData = new InventoryItemData();
                itemData.itemTableID = items[i].uid;
                // ������ ��ü�� RefrshSlot �޼ҵ忡 �����մϴ�.
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

        RefreshData(0, CPUSlotList);  // CPU ī�װ��� index 0���� ����
    }
    private void OnButtonGpu()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        SKILLPage.SetActive(false);

        RefreshData(1, GPUSlotList);  // GPU ī�װ��� index 1���� ����
    }
    private void OnButtonSkill()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(true);

        RefreshData(2, SKILLSlotList);  // SKILL ī�װ��� index 2���� ����
    }
}
