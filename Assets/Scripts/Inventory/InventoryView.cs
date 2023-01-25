using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
namespace Inventory{
public class InventoryView : MonoBehaviour
{   
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private ItemType targetItemsType;
    public ItemType TargetItemsType{get => targetItemsType; set => targetItemsType = value;}
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private GameObject deselectCellPrefab;
    [SerializeField]
    private GameObject itemInCellPrefab;
    [SerializeField]
    private Transform cellRoot;
    [SerializeField]
    private TMP_Text targetItemText;

    private Dictionary<ItemInCellUI,InventoryCell> itemToCell = new Dictionary<ItemInCellUI, InventoryCell>();
    private Dictionary<InventoryCell,ItemInCellUI> cellToItem = new Dictionary<InventoryCell, ItemInCellUI>();

    [SerializeField]
    private List<InventoryCell> inventoryCells = new List<InventoryCell>();
    private InventoryManager inventoryManager => InventoryManager.Instance;
    private List<Item> CurrentItems => inventoryManager.Items;


    [SerializeField]
    private ItemMoreInfoPanel itemMoreInfoPanel;

    [SerializeField]
    private float inventoryYpos = 0;
    private CustomizeItemSelector currentItemSelector;
    public CustomizeItemSelector CurrentItemSelector { get => currentItemSelector; set => currentItemSelector = value; }

    private GameObject deselectCell;

    public void Initialize()
    {   
        //AddDeselectCell
        deselectCell = Instantiate(deselectCellPrefab,cellRoot);
        // inventoryCells.AddRange(new InventoryCell[inventoryManager.ItemsMaxCount]);
        // inventoryManager.onItemsLoaded += InitializeUIs;
        inventoryManager.onInventoryChange += UpdateUI;
        HideInventory(false);
    }
    private void ClearView()
    {
        //Remove all UI's before update
        itemToCell.Clear();
        cellToItem.Clear();
        foreach(InventoryCell inventoryCell in inventoryCells)
        {
            Destroy(inventoryCell.gameObject);
        }
        inventoryCells.Clear();
    }
    public void ShowInventory(RectTransform targetRect, ItemType targetItems = ItemType.all, UIElementSide inventorySide = UIElementSide.right, UIElementSide morePanelSide = UIElementSide.right, CustomizeItemSelector itemSelector = null)
    {
        GlobalEventsManager.Instance.menuEvents.onCustomizationInventoryActivate?.Invoke(true);
        CalculateInventoryAnchorPosition(targetRect,inventorySide,morePanelSide);
        panel.SetActive(true);
        TargetItemsType = targetItems;
        currentItemSelector = itemSelector;
        UpdateUI();
        EventsSystemHelper.SelectUIElement(deselectCell);
        
    }
    void CalculateInventoryAnchorPosition(RectTransform targetRect, UIElementSide inventorySide, UIElementSide morePanelSide)
    {
        RectTransform inventoryRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform moreInfoRectTransform = itemMoreInfoPanel.GetComponent<RectTransform>();
        
        bool isInventoryLeft = inventorySide == UIElementSide.left;
        bool isMorePanelLeft = morePanelSide == UIElementSide.left;
        
        inventoryRectTransform.pivot = new Vector2(isInventoryLeft ? 1.15f : -0.15f,0.5f);
        moreInfoRectTransform.pivot = new Vector2(isMorePanelLeft ? 1.5f : -0.5f,0);

        inventoryRectTransform.position = targetRect.position;

        Vector3 anchPos = inventoryRectTransform.anchoredPosition;
        anchPos.y = inventoryYpos;
        inventoryRectTransform.anchoredPosition = anchPos;

        moreInfoRectTransform.anchoredPosition = Vector2.zero;

    }

    public void ShowMorePanel(Item item)
    {
        itemMoreInfoPanel.Initialize(item);
    }
    public void HideInventory(bool selectInventoryUI = true)
    {
        if(panel.activeSelf == true)
        {
            currentItemSelector = null;
            panel.SetActive(false);
            HideMorePanel();
            GlobalEventsManager.Instance.menuEvents.onCustomizationInventoryActivate?.Invoke(false);
            if(selectInventoryUI)
                EventsSystemHelper.SelectUIElement(HistoryManager.prevEventsSystemSelectedGameObject);
        }
    }
    public void SelectItem(Item item)
    {
        if(currentItemSelector != null)
        {
            currentItemSelector.SelectItem(item);
        }
    }
    public void DeselectItem()
    {
        currentItemSelector?.DeselectItem();
    }
    public void HideMorePanel()
    {
        itemMoreInfoPanel.Hide();
    }
    private void InitializeUIs()
    {
        ClearView();
        // initialize every item and put it in inventory cells or equip cells;
        foreach(Item item in CurrentItems)
        {
            if(item.ItemType == targetItemsType || targetItemsType == ItemType.all)
            {
                InventoryCell inventoryCell = Instantiate(cellPrefab,cellRoot).GetComponent<InventoryCell>();
                ItemInCellUI itemInCellUI = Instantiate(itemInCellPrefab,inventoryCell.transform).GetComponent<ItemInCellUI>();
                itemInCellUI.Initialize(item);
                itemInCellUI.SetCell(inventoryCell);
                inventoryCells.Add(inventoryCell);
                itemToCell.Add(itemInCellUI,inventoryCell);
                cellToItem.Add(inventoryCell,itemInCellUI);
            }
        }
        SetupCellsNavigation();
    }
    void SetupCellsNavigation()
    {
        int cellsInRow = 4;
        List<Selectable> allSelectable = new List<Selectable>();

        allSelectable.Add(deselectCell.GetComponent<Selectable>());
        foreach(InventoryCell cell in inventoryCells)
            allSelectable.Add(cell.GetComponent<Selectable>()); 
        
        for (int i = 0; i < allSelectable.Count; i++)
        {   
            Selectable selectable = allSelectable[i];
            
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            
            nav.selectOnUp = i-cellsInRow >= 0 ? allSelectable[i-cellsInRow] : null;
            nav.selectOnDown = i+cellsInRow < allSelectable.Count ? allSelectable[i+cellsInRow] : null;
            nav.selectOnLeft = i-1 >= 0 ? allSelectable[i-1] : null;
            nav.selectOnRight = i+1 < allSelectable.Count ? allSelectable[i+1] : null;

            selectable.navigation = nav;
        }
        Navigation cycleNavFirst = allSelectable[0].navigation;
        cycleNavFirst.mode = Navigation.Mode.Explicit;
        Navigation cycleNavLast = allSelectable[allSelectable.Count-1].navigation;
        cycleNavLast.mode = Navigation.Mode.Explicit;

        cycleNavFirst.selectOnLeft = allSelectable[allSelectable.Count-1];
        cycleNavLast.selectOnRight = allSelectable[0];

        allSelectable[allSelectable.Count-1].navigation = cycleNavLast;
        allSelectable[0].navigation = cycleNavFirst;
    }
    private void UpdateUI()
    {
        InitializeUIs();
    }
    public void SortItems()
    {
        inventoryManager.SortItems();
    }
}
}