using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public int uid;         // 중복이 안도도록 고유한 Item ID ( 아이템 습득시에 GameManager에서 생성)
    public int itemTableID; // 테이블 참조를 하기 위한 Table ID
    public int amount;      // 습득한 아이템중에서 중첩이 가능한 아이템의 보유 갯수. ( 예 : 물약99개 )
}

[System.Serializable]


public class Inventory
{
    private int maxSlotCount = 18; // 인벤토리 최대 갯수.
    public int MaxSlot { get => maxSlotCount; }

    private int curSlotCount;
    public int CurSlot
    {
        get => curSlotCount;
        set => curSlotCount = value;
    }

    [SerializeField]
   public List<InventoryItemData> items = new List<InventoryItemData>(); // 실제 아이템 관리 리스트

    public void AddItem(InventoryItemData newItem)
    {
        int index = FindItemIndex(newItem);

        if (GameManager.Instance.GetItemData(newItem.itemTableID, out object item))
        {
            items[index].amount += newItem.amount; // 갯수만 증가.
        }
    }

    // 인벤토리에 여유 슬롯이 있는지 확인
    public bool isFull()
    {
        return curSlotCount >= maxSlotCount;
    }

    // 외부에 아이템 리스트를 반환
    public List<InventoryItemData> GetItemList()
    {
        curSlotCount = items.Count;
        return items;
    }

    // 인벤토리내에 같은 tableID를 가지고 있는 아이템이 있다면, 몇번째 Index에 있는지를 반환
    private int FindItemIndex(InventoryItemData newItem)
    {
        int result = -1;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemTableID == newItem.itemTableID)
            {
                result = i;
                return result; // 겹치는 아이템을 찾았다.
            }
        }
        return result; // 아이템을 찾지 못해서 -1 return.
    }

    // 잡화상점에 아이템을 팔았을때 인벤토리 내에서 해당 아이템을 제거 하는 함수
    public void DeleteItem(InventoryItemData deleteItem)
    {
        int index = FindItemIndex(deleteItem);
        if (-1 < index)
        {
            items[index].amount -= deleteItem.amount;
            if (items[index].amount < 1)
            {
                items.RemoveAt(index);// 해당 index에 해당하는 요소를 삭제
                //items.Remove(); // 동일한 요소를 삭제
                curSlotCount--;
            }
        }
    }

    //강화와 같은 인벤토리내 보유한 아이템의 정보를 새롭게 변경해야 할때 호출.
    //newData.uid가 일치하는 아이템이 items에 있을 경우 데이터를 갱신
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
