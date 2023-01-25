using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory;
using System.Linq;
using Random = UnityEngine.Random;
[Serializable]
public class EquippedItemsPreset
{
    [SerializeField]
    private ItemPreset face = new ItemPreset(ItemType.face);
    [SerializeField]
    private ItemPreset ring1 = new ItemPreset(ItemType.ring);
    [SerializeField]
    private ItemPreset ring2 = new ItemPreset(ItemType.ring,1);
    [SerializeField]
    private ItemPreset talisman = new ItemPreset(ItemType.talisman);
    [SerializeField]
    private ItemPreset staff = new ItemPreset(ItemType.staff);
    private List<ItemPreset> allItemPresets = new List<ItemPreset>();
    public List<ItemPreset> AllItemPresets { get => allItemPresets; }
    public EquippedItemsPreset()
    {
        allItemPresets.AddRange(
           new ItemPreset[]{face,ring1,ring2,talisman,staff}
        );
    }

    public void ResetAllPresets()
    {
        foreach(ItemPreset preset in allItemPresets)
            preset.RemoveItem();
    }
    public ItemPreset SelectPreset(ItemType type, int itemIndex = 0)
    {
        ItemPreset preset = allItemPresets.Find(p=> p.type == type && p.itemIndex == itemIndex);
        return preset;
    }
}
[Serializable]
public class ItemPreset
{
    public Item item;
    public ItemType type;
    public int itemIndex;
    public ItemPreset(ItemType type, int itemIndex = 0)
    {
        this.type = type;
        this.itemIndex = itemIndex;
    }
    public void PutItem(Item item)
    {
        this.item = item;
    }
    public void RemoveItem()
    {
        this.item = null;
    }
    public Item GetRandomItem()
    {
        List<Item> itemsThisType = InventoryManager.Instance.Items.Where((i)=> i.ItemType == type).ToList();
        if(itemsThisType.Count > 0)
            return itemsThisType[Random.Range(0,itemsThisType.Count)];    
        else return null;
    }
}
