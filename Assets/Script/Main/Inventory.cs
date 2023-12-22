using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public int uid;         // �ߺ��� �ȵ����� ������ Item ID ( ������ ����ÿ� GameManager���� ����)
    public int itemTableID; // ���̺� ������ �ϱ� ���� Table ID
    public int amount;      // ������ �������߿��� ��ø�� ������ �������� ���� ����. ( �� : ����99�� )
}

[System.Serializable]


public class Inventory
{
    private int maxSlotCount = 18; // �κ��丮 �ִ� ����.
    public int MaxSlot { get => maxSlotCount; }

    private int curSlotCount;
    public int CurSlot
    {
        get => curSlotCount;
        set => curSlotCount = value;
    }

    [SerializeField]
   public List<InventoryItemData> items = new List<InventoryItemData>(); // ���� ������ ���� ����Ʈ

    public void AddItem(InventoryItemData newItem)
    {
        int index = FindItemIndex(newItem);

        if (GameManager.Instance.GetItemData(newItem.itemTableID, out object item))
        {
            items[index].amount += newItem.amount; // ������ ����.
        }
    }

    // �κ��丮�� ���� ������ �ִ��� Ȯ��
    public bool isFull()
    {
        return curSlotCount >= maxSlotCount;
    }

    // �ܺο� ������ ����Ʈ�� ��ȯ
    public List<InventoryItemData> GetItemList()
    {
        curSlotCount = items.Count;
        return items;
    }

    // �κ��丮���� ���� tableID�� ������ �ִ� �������� �ִٸ�, ���° Index�� �ִ����� ��ȯ
    private int FindItemIndex(InventoryItemData newItem)
    {
        int result = -1;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemTableID == newItem.itemTableID)
            {
                result = i;
                return result; // ��ġ�� �������� ã�Ҵ�.
            }
        }
        return result; // �������� ã�� ���ؼ� -1 return.
    }

    // ��ȭ������ �������� �Ⱦ����� �κ��丮 ������ �ش� �������� ���� �ϴ� �Լ�
    public void DeleteItem(InventoryItemData deleteItem)
    {
        int index = FindItemIndex(deleteItem);
        if (-1 < index)
        {
            items[index].amount -= deleteItem.amount;
            if (items[index].amount < 1)
            {
                items.RemoveAt(index);// �ش� index�� �ش��ϴ� ��Ҹ� ����
                //items.Remove(); // ������ ��Ҹ� ����
                curSlotCount--;
            }
        }
    }

    //��ȭ�� ���� �κ��丮�� ������ �������� ������ ���Ӱ� �����ؾ� �Ҷ� ȣ��.
    //newData.uid�� ��ġ�ϴ� �������� items�� ���� ��� �����͸� ����
    public void UpdateitemInfo(InventoryItemData newData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].uid == newData.uid)
            {
                items[i].itemTableID = newData.itemTableID;
                items[i].amount = newData.amount;
            }
        }
    }
}
