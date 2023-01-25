using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ItemsHolderSO", menuName = "Items/ItemsHolderSO", order = 0)]
public class ItemsHolderSO : ScriptableObject {
    [SerializeField]
    private List<ItemSO> allItems = new List<ItemSO>();
    public List<ItemSO> AllItems => allItems;

    public Item GetItemByID(string id) => allItems.Find(i=>i.Item.Id == id).Clone();
}