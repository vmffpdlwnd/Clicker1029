using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public enum ShopCategory { CPU, GPU, DECO, SKILL }

    public class ShopItem
    {
        public int CPULevel { get; private set; }
        public string ItemName { get; private set; }
        public int ItemPrice { get; private set; }

        public ShopItem(int cpuLevel, string itemName, int itemPrice)
        {
            CPULevel = cpuLevel;
            ItemName = itemName;
            ItemPrice = itemPrice;
        }
    }

    private Dictionary<ShopCategory, List<ShopItem>> shopItems = new Dictionary<ShopCategory, List<ShopItem>>();
    private Dictionary<ShopCategory, bool> isCategoryReady = new Dictionary<ShopCategory, bool>();

    private void Start()
    {
        InitializeShopItems();
    }

    private void InitializeShopItems()
    {
        shopItems.Add(ShopCategory.CPU, new List<ShopItem>()
        {
            new ShopItem(1, "Pentium Silver", 100),
            new ShopItem(2, "Pentium Gold", 200),
            new ShopItem(3, "I3", 300),
            new ShopItem(4, "I5", 400),
            new ShopItem(5, "I7", 500)
        });

        shopItems.Add(ShopCategory.GPU, new List<ShopItem>());
        shopItems.Add(ShopCategory.DECO, new List<ShopItem>());
        shopItems.Add(ShopCategory.SKILL, new List<ShopItem>());

        isCategoryReady.Add(ShopCategory.CPU, true);
        isCategoryReady.Add(ShopCategory.GPU, false);
        isCategoryReady.Add(ShopCategory.DECO, false);
        isCategoryReady.Add(ShopCategory.SKILL, false);
    }

    public List<ShopItem> GetShopItems(ShopCategory category)
{
    if (isCategoryReady[category])
    {
        return shopItems[category];
    }
    else
    {
        // 준비중인 카테고리에 대한 처리
        return new List<ShopItem>() { new ShopItem(0, "준비중", 0) };
    }
}

}
