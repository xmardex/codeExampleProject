using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveManager : Singleton<LoadSaveManager>
{
    void Awake()
    {
        base.SetAsCrossScene();
    }
    public InventorySaveData LoadInventory()
    {
        InventorySaveData inventorySaveData = PersistentCache.TryLoad<InventorySaveData>("Inventory");
        inventorySaveData ??= new InventorySaveData();
        return inventorySaveData;
    }
    public void SaveInventory(List<Item> itemsToSave)
    {
        InventorySaveData inventorySavedData = new InventorySaveData();
        if(inventorySavedData != null)
        {
            inventorySavedData.SavedItemsIDs.Clear();
        }
        inventorySavedData.SavedItemsIDs ??= new List<string>();

        foreach(Item item in itemsToSave)
        {
            inventorySavedData.SavedItemsIDs.Add(item.Id);
        }
        PersistentCache.Save<InventorySaveData>("Inventory",inventorySavedData);
    }
}
