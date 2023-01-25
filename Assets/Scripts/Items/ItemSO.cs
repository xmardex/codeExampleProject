using UnityEngine;

[CreateAssetMenu(fileName = "_item", menuName = "Items/ItemSO", order = 0)]
public class ItemSO : ScriptableObject 
{
    [SerializeField]
    private Item item;
    public Item Item => item;
    public Item Clone() => item.Clone();
}