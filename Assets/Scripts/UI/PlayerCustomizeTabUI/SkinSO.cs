using UnityEngine;
[CreateAssetMenu(fileName = "_skin", menuName = "PlayerSkins/SkinSO", order = 0)]
public class SkinSO : ScriptableObject 
{
    [SerializeField]
    private string id;
    [SerializeField]
    private string skinName;
    [SerializeField]
    private Sprite avatarSprite;
    [SerializeField]
    private GameObject skinPrefab;

    public string Id { get => id; }
    public string SkinName { get => skinName;}
    public Sprite AvatarSprite { get => avatarSprite;}
    public GameObject SkinPrefab { get => skinPrefab;}

}