using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RigBonesManager : MonoBehaviour
{

    // ------- Bones -------
    private RigBone head;
    private RigBone spine;
    private RigBone leftHand;
    private RigBone rightHand;
    private RigBone leftFoot;
    private RigBone rightFoot;
    private List<RigBone> allBones;

    // ------- VFX Prefabs -------
    [SerializeField]
    private GameObject handVFX;
    [SerializeField]
    private Vector3 spellRootOffset;
    public Vector3 GetSpellRootPosition() => transform.position + spellRootOffset;

    public void Initialize(Transform rootRigTransform, EquippedItemsPreset equippedItemsPreset)
    {
        BonesInitialize(rootRigTransform);
        ItemsPrefabsInitialize(equippedItemsPreset);
        VFXPrefabsInitialize();
    }
    void BonesInitialize(Transform rootRigTransform)
    {
        head = new RigBone("Head",rootRigTransform);
        spine = new RigBone("Spine2",rootRigTransform);
        leftHand = new RigBone("LeftHand",rootRigTransform,handVFX);
        rightHand = new RigBone("RightHand",rootRigTransform,handVFX);
        leftFoot = new RigBone("LeftFoot",rootRigTransform);
        rightFoot = new RigBone("RightFoot",rootRigTransform);
        
        allBones ??= new List<RigBone>();
        allBones.AddRange(new RigBone[]{head,spine,leftHand,rightHand,leftFoot,rightFoot});
    }
    void VFXPrefabsInitialize()
    {
        foreach(RigBone rigBone in allBones)
        {
            if(rigBone.vfx_prefab != null)
                rigBone.vfx_instance = Instantiate(rigBone.vfx_prefab,rigBone.bone);
        }
    }   
    void ItemsPrefabsInitialize(EquippedItemsPreset equippedItemsPreset)
    {
        int ringIndex = 0;
        foreach(ItemPreset itemPreset in equippedItemsPreset.AllItemPresets)
        {
            Item item = itemPreset.item;
            if(item != null && item.ItemPrefab.prefab != null)
            {
                ItemPrefab itemPrefab = item.ItemPrefab;
                switch(item.ItemType)
                {
                    case ItemType.face:
                    {
                        Transform itemInstance = Instantiate(itemPrefab.prefab,head.bone).transform;
                        ItemPosition(itemInstance,itemPrefab);
                    }
                    break;
                    case ItemType.ring:
                    {
                        Transform hand = ringIndex == 0 ? leftHand.bone : rightHand.bone;
                        Transform itemInstance = Instantiate(itemPrefab.prefab,hand).transform;
                        ItemPosition(itemInstance,itemPrefab);
                        ringIndex++;
                    }
                    break;
                    case ItemType.talisman:
                    {
                        Transform itemInstance = Instantiate(itemPrefab.prefab,spine.bone).transform;
                        ItemPosition(itemInstance,itemPrefab);
                    }
                    break;
                    case ItemType.staff:
                    {
                        Transform itemInstance = Instantiate(itemPrefab.prefab,rightHand.bone).transform;
                        ItemPosition(itemInstance,itemPrefab);
                    }
                    break;
                }
            }
        }
    }
    void ItemPosition(Transform itemInstance, ItemPrefab itemPrefab)
    {
        Vector3 itemPos = itemInstance.localPosition + itemPrefab.posOffset;
        Vector3 itemRot = itemInstance.localEulerAngles + itemPrefab.rotOffset;
        itemInstance.localPosition = itemPos;
        itemInstance.localEulerAngles = itemRot;
        itemInstance.localScale = itemPrefab.newScale;
    }

}
public class RigBone
{
    public string boneName;
    public Transform bone;
    public GameObject vfx_prefab;
    public GameObject vfx_instance;
    
    public RigBone(string boneName, Transform rig, GameObject vfx = null)
    {
        List<Transform> allTransforms = new List<Transform>();
        allTransforms.AddRange(rig.GetComponentsInChildren<Transform>());

        this.boneName = boneName;
        bone = allTransforms.Find(t=>t.name == boneName);
        vfx_prefab = vfx;
    }
}