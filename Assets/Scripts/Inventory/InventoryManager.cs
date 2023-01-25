using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
namespace Inventory{
public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField]
    private List<Item> items = new List<Item>();
    public List<Item> Items => items;

    [SerializeField]
    private static InventoryView inventoryView;
    public static InventoryView InventoryView => inventoryView;

    [SerializeField]
    private ItemsHolderSO itemsHolderSO;
    // [SerializeField]
    // private int itemsMaxCount; // 0 = infinty (flexible inventory capacity)
    // public int ItemsMaxCount { get => itemsMaxCount; set => itemsMaxCount = value; }
    public UnityAction onItemsLoaded;
    public UnityAction onInventoryChange;
    public UnityAction onItemsSaved;
    private void Awake() 
    {
        inventoryView = GetComponentInChildren<InventoryView>(true); 
        inventoryView.Initialize();
        LoadItems();
    }
    public void AddItem(Item item)
    {
        items.Add(item);
        SortItems();
        onInventoryChange?.Invoke();
        SaveItems();
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        SortItems();
        onInventoryChange?.Invoke();
        SaveItems();
    }
    public void SortItems()
    {
        items.Sort((a,b) => a.Name.CompareTo(b.Name));
        // onInventoryChange?.Invoke();
        // SaveItems();
    }
    public void LoadItems()
    {
        items?.Clear();

        //Load available items
        InventorySaveData inventorySaveData = LoadSaveManager.Instance.LoadInventory();
        foreach(string itemID in inventorySaveData.SavedItemsIDs)
        {
            items.Add(itemsHolderSO.GetItemByID(itemID));
        }
        onItemsLoaded?.Invoke(); 
    }
    public void SaveItems()
    {
        //Save items
        LoadSaveManager.Instance.SaveInventory(items);
        onItemsSaved?.Invoke();
    }

}
}