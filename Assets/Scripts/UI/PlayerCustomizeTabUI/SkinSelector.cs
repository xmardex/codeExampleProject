using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private Button nextBtn,prevBtn;
    [SerializeField]
    private Image skinPreview;
    [SerializeField]
    private TMP_Text skinName;
    public Action<SkinSO> onSkinChanged;
    private SkinSO currentSkin;
    private int currentSkinIndex;
    private int lastSkinIndex => skinsHolderSO.allSkins.Count-1;

    [SerializeField]
    private SkinsHolderSO skinsHolderSO;

    void Awake()
    {
        //set default skin
        SetDefault();
        Initialize();
    }
    public void SetDefault()
    {
        currentSkinIndex = 0;
        currentSkin = skinsHolderSO.allSkins[currentSkinIndex];
        ChangeSkin(currentSkin);

        ChangeSkin(skinsHolderSO.allSkins[0]);
    }
    void Initialize()
    {
        nextBtn.onClick.AddListener(NextSkin);
        prevBtn.onClick.AddListener(PrevSkin);
    }
    public void NextSkin()
    {
        currentSkinIndex = currentSkinIndex == lastSkinIndex ? 0 : ++currentSkinIndex;
        
        currentSkin = skinsHolderSO.allSkins[currentSkinIndex];
        ChangeSkin(currentSkin);
    }
    public void PrevSkin()
    {
        currentSkinIndex = currentSkinIndex == 0 ? lastSkinIndex : --currentSkinIndex;

        currentSkin = skinsHolderSO.allSkins[currentSkinIndex];
        ChangeSkin(currentSkin);
    }
    public void RandomSkin()
    {
        int randomSkinIndex = Random.Range(0,skinsHolderSO.allSkins.Count);
        SkinSO randomSkin = skinsHolderSO.allSkins[randomSkinIndex];
        currentSkinIndex = randomSkinIndex;
        ChangeSkin(randomSkin);
    }
    void ChangeSkin(SkinSO skin)
    {
        onSkinChanged?.Invoke(skin);
        skinPreview.sprite = skin.AvatarSprite;
        skinName.text = skin.SkinName;
    }
}
