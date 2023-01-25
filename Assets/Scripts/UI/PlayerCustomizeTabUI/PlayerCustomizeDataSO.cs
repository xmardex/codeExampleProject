using UnityEngine;
using System;
[CreateAssetMenu(fileName = "_customizeData", menuName = "PlayerData/CustomizeSO", order = 0)]
public class PlayerCustomizeDataSO : ScriptableObject 
{
    [SerializeField]
    private int playerNum;
    public int PlayerNum => playerNum;

    [SerializeField]
    private bool isSet;
    public bool IsSet { get => isSet; set => isSet = value; }
    

    [SerializeField]
    private SkinSO playerSkin;
    public SkinSO PlayerSkin { get => playerSkin; set => playerSkin = value; }
    

    [SerializeField]
    private EquippedItemsPreset equippedItemsPreset = new EquippedItemsPreset();
    public EquippedItemsPreset EquippedItemsPreset { get => equippedItemsPreset;}
    
}