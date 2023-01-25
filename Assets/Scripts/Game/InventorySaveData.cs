using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySaveData
{
    [SerializeField]
    public List<string> SavedItemsIDs = new List<string>();
}
