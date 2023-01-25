using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Inventory{
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , ISelectHandler , IDeselectHandler
{
	[SerializeField]
	private ItemInCellUI currentItemInCellUI;
	public ItemInCellUI CurrentItemInCellUI {set => currentItemInCellUI = value; }

	private Button button;
	public Button Btn { get => button; set => button = value; }

	[SerializeField]
	private Color hoveredColor;
	private Color normColor;
	[SerializeField]
	private Color selectedColor;

	private bool isSelected;

	private CanvasGroup canvasGroup;
	private void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();
		
		button = GetComponent<Button>();
		normColor = button.image.color;
		button.onClick.AddListener(SelectItem);
	}
	private void OnDestroy() 
	{
		button.onClick.RemoveListener(SelectItem);
	}
	// public void OnDrop(PointerEventData eventData)
	// {
	//     ItemInCellUI newItemUI;
	//     if(!eventData.pointerDrag.TryGetComponent<ItemInCellUI>(out newItemUI)) return;

	//     SwitchItemsInCells(this,newItemUI.CurrentInventoryCell);

	// }
	// public static void SwitchItemsInCells(InventoryCell a, InventoryCell b)
	// {
	//     a.CurrentItemInCellUI?.SetCell(b);
	//     b.CurrentItemInCellUI?.SetCell(a);
	// }
	public void Initialize()
	{
		int? playerNum = InventoryManager.InventoryView.CurrentItemSelector?.PlayerNum;
		if(playerNum.HasValue)
		{
			isSelected = currentItemInCellUI.CurrentItem.GetCanBeSelected(playerNum.Value);
			if(isSelected)
			{
				canvasGroup.interactable = true;
				// canvasGroup.alpha = 1f;
				button.image.color = normColor;
			}
			else
			{
				canvasGroup.interactable = false;
				// canvasGroup.alpha = 0.65f;
				button.image.color = selectedColor;
			}
		}
	}
	public void SelectItem()
	{
		InventoryManager.InventoryView.SelectItem(currentItemInCellUI.CurrentItem);
	}
	void OpenMorePanel()
	{
		InventoryManager.InventoryView.ShowMorePanel(currentItemInCellUI.CurrentItem);
		button.image.color = hoveredColor;
	}
	void HideMorePanel()
	{
		InventoryManager.InventoryView.HideMorePanel();
		button.image.color = isSelected ? normColor : selectedColor;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		OpenMorePanel();
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		HideMorePanel();
	}
	public void OnSelect(BaseEventData eventData)
	{
		OpenMorePanel();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		HideMorePanel();
	}
}
}