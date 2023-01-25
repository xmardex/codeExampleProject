using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inventory;
using System;
[RequireComponent(typeof(Button))]
public class CustomizeItemSelector : MonoBehaviour
{
    private TMP_Text itemNameHolder;
    private Image itemIconHolder;
    private Item selectedItem;

    private int playerNum;

    [Space]
    [Header("Inventory placement")]
    [SerializeField]
    private UIElementSide inventorySide;
    [SerializeField]
    private UIElementSide morePanelSide;

    [SerializeField]
    private ItemType targetItemType;
    private int itemIndex;
    private Button button;
    private PlayerCustomizeController playerCustomizeController;

    public int PlayerNum { get => playerNum; set => playerNum = value; }
    public int ItemIndex { set => itemIndex = value; }
    public ItemType TargetItemType {get => targetItemType; }

    void Awake()
    {
        playerCustomizeController = GetComponentInParent<PlayerCustomizeController>();
        button = GetComponent<Button>();
        itemNameHolder = GetComponentInChildren<TMP_Text>();
        itemIconHolder = GetComponentsInChildren<Image>()[1];
        itemNameHolder.gameObject.SetActive(true);
        itemIconHolder.gameObject.SetActive(false);
        button.onClick.AddListener(ShowInventory);
    }
    public void ShowInventory()
    {
        RectTransform buttonRect = GetComponent<RectTransform>();
        InventoryManager.InventoryView.ShowInventory(buttonRect, targetItemType, inventorySide, morePanelSide, this);
    }
    public void SelectItemRandom()
    {
        ItemPreset itemPreset = playerCustomizeController.PlayerData.data.EquippedItemsPreset.SelectPreset(targetItemType,itemIndex);
        Item randomItem = itemPreset.GetRandomItem();
        if(randomItem !=null)
        {
            PutItemInCell(randomItem);
            itemPreset.PutItem(randomItem);
        }
    }
    public void SelectItem(Item item)
    {
        ItemPreset itemPreset = playerCustomizeController.PlayerData.data.EquippedItemsPreset.SelectPreset(targetItemType,itemIndex);
        PutItemInCell(item);
        itemPreset.PutItem(item);
    }
    public void PutItemInCell(Item item)
    {
        if(selectedItem != null)
        {
            selectedItem.SetCanBeSelected(playerNum,true);
        }
        item.SetCanBeSelected(playerNum,false);

        selectedItem = item;
        itemNameHolder.gameObject.SetActive(false);
        itemIconHolder.gameObject.SetActive(true);
        itemIconHolder.sprite = item.Sprite;
        InventoryManager.InventoryView.HideInventory();

    }
    private void OnDestroy() {
        button.onClick.RemoveListener(ShowInventory);
    }

    public void DeselectItem()
    {
        ItemPreset itemPreset = playerCustomizeController.PlayerData.data.EquippedItemsPreset.SelectPreset(targetItemType,itemIndex);
        
        if(selectedItem != null)
        {   
            selectedItem.SetCanBeSelected(playerNum,true);
            selectedItem = null;
        }
        
        itemNameHolder.gameObject.SetActive(true);
        itemIconHolder.gameObject.SetActive(false);
    }
}
