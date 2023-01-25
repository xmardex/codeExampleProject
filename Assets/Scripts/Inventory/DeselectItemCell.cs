using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
[RequireComponent(typeof(Button))]
public class DeselectItemCell : MonoBehaviour
{   
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Deselect);
    }
    public void Deselect()
    {
        InventoryManager.InventoryView.DeselectItem();
        InventoryManager.InventoryView.HideInventory();
    }
    void OnDestroy()
    {
        button.onClick.RemoveListener(Deselect);
    }
}
