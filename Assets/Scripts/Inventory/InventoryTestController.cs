using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.SceneManagement;
public class InventoryTestController : MonoBehaviour
{
    [SerializeField]
    private List<ItemSO> itemsToAdd = new List<ItemSO>();
    [SerializeField]
    private ItemsHolderSO itemsHolderSO;

    [SerializeField]
    private bool addItemsFromList;
    [SerializeField]
    private bool addItemsFromHolder;

    public void AddItems()
    {
        if(addItemsFromList)
        foreach(ItemSO itemSO in itemsToAdd)
            InventoryManager.Instance.AddItem(itemSO.Clone());
        
        if(addItemsFromHolder)
        foreach(ItemSO itemSO in itemsHolderSO.AllItems)
            InventoryManager.Instance.AddItem(itemSO.Clone());
    }
    public void ClearInventory()
    {
        PersistentCache.ClearPersistentStorage();
        PersistentCache.ClearResourcesStorage();
    }
}
