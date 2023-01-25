using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Inventory{

[RequireComponent(typeof(CanvasGroup))]
public class ItemInCellUI : MonoBehaviour
{
        [SerializeField]
        private Item currentItem;
        [SerializeField]
        private InventoryCell currentInventoryCell;
        
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Canvas canvas;
        [SerializeField]
        private Image itemImage;
        public InventoryCell CurrentInventoryCell {set => currentInventoryCell = value;}
        public Item CurrentItem { get => currentItem; }

        public void Initialize(Item item)
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            currentItem = item;
            itemImage.sprite = currentItem.Sprite;
        }

        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     canvasGroup.blocksRaycasts = false;
        //     canvasGroup.alpha = 0.7f;
        //     transform.SetParent(InventoryView.Instance.Panel.transform);
        //     transform.SetAsLastSibling();
        // }

        // public void OnDrag(PointerEventData eventData)
        // {
        //     rectTransform.anchoredPosition +=eventData.delta / canvas.scaleFactor;
        // }

        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     canvasGroup.blocksRaycasts = true;
        //     canvasGroup.alpha = 1f;
        //     transform.SetParent(currentInventoryCell.transform);
        //     transform.SetAsFirstSibling();
        //     SetCell(currentInventoryCell);
        // }

        public void SetCell(InventoryCell cell)
        {   
            currentInventoryCell = cell;
            transform.SetParent(currentInventoryCell.transform);
            transform.localPosition = Vector3.zero;
            cell.CurrentItemInCellUI = this;
            cell.Initialize();
        }

    }
        
}
