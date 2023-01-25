using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    [SerializeField]
    private string id;
    [SerializeField]
    private string name;
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private ItemPrefab prefab;
    [Space(50)]
    [SerializeField]
    private PlayerStats playerStatModifier;
    [Space(50)]
    [SerializeField]
    private PlayerStats enemyStatModifier;
    [Space(50)]
    [HideInInspector]
    private bool canBeSelected_p1;
    [HideInInspector]
    private bool canBeSelected_p2;
    public Item(string id, string name, ItemType itemType, Sprite sprite, ItemPrefab prefab, PlayerStats playerStatModifier, PlayerStats enemyStatModifier, bool canBeSelected_p1, bool canBeSelected_p2)
    {
        this.id = id;
        this.name = name;
        this.itemType = itemType;
        this.sprite = sprite;
        this.prefab = prefab;
        this.playerStatModifier = playerStatModifier;
        this.enemyStatModifier = enemyStatModifier;
        this.canBeSelected_p1 = true;
        this.canBeSelected_p2 = true;
    }

    public Item Clone() => new Item(this.id,this.name, this.itemType, this.sprite,this.prefab,this.playerStatModifier.Clone(),this.enemyStatModifier.Clone(),this.canBeSelected_p1,this.canBeSelected_p2);

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public ItemType ItemType { get => itemType;}
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public ItemPrefab ItemPrefab { get => prefab; set => prefab = value; }
    public PlayerStats PlayerStatModifier { get => playerStatModifier; set => playerStatModifier = value; }
    public PlayerStats EnemyStatModifier { get => enemyStatModifier; set => enemyStatModifier = value; }
    public bool CanBeSelected_P1 { get => canBeSelected_p1; set => canBeSelected_p1 = value; }
    public bool CanBeSelected_P2 { get => canBeSelected_p2; set => canBeSelected_p2 = value; }
    public bool GetCanBeSelected(int playerNum)
    {
        if(playerNum == 1)
            return canBeSelected_p1;
        if(playerNum == 2)
            return canBeSelected_p2;

        Debug.LogError("There is no such playerNum");
        return false;
    }
    public void SetCanBeSelected(int playerNum, bool canBeSelected)
    {
        if(playerNum == 1)
            canBeSelected_p1 = canBeSelected;
        if(playerNum == 2)
            canBeSelected_p2 = canBeSelected;
    }
}
[Serializable]
public class ItemPrefab
{
    public GameObject prefab;
    public Vector3 posOffset;
    public Vector3 rotOffset;
    public Vector3 newScale = Vector3.one;
}