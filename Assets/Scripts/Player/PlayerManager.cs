using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory;
using GameManagment;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerStatsController))]
[RequireComponent(typeof(RigBonesManager))]
[RequireComponent(typeof(PlayerStateManager))]
[RequireComponent(typeof(PlayerSpellInputController))]
[RequireComponent(typeof(PlayerSpellController))]
public class PlayerManager : BaseScript, IPlayer
{
    private PlayerStatsController playerStatsController;
    public PlayerStatsController PlayerStatsController => playerStatsController;
    private PlayerAnimationController playerAnimationController;
    public PlayerAnimationController PlayerAnimationController => playerAnimationController;
    private PlayerSpellInputController playerSpellInputController;
    public PlayerSpellInputController PlayerSpellInputController => playerSpellInputController;
    private PlayerSpellController playerSpellCaster;
    public PlayerSpellController PlayerSpellCaster => playerSpellCaster;

    [HideInInspector]
    public PlayerManager opponentPlayer;

    [SerializeField]
    private PlayerCustomizeDataSO customizedData;
    [SerializeField]
    private Transform rigRoot;
    public Transform RigRoot=>rigRoot;

    [SerializeField]
    private Image playerAvatarImage;

    private RigBonesManager rigBonesManager;
    public RigBonesManager RigBonesManager => rigBonesManager;

    [SerializeField]
    private bool isAIControlled;
    public bool IsAIControlled{get => isAIControlled;set => isAIControlled = value;}

    private PlayerStateManager playerStateManager;
    public PlayerStateManager PlayerStateManager => playerStateManager;

    public int PlayerNum => customizedData.PlayerNum;

    private bool playerFinishCurrentRoundResolution;
    public bool PlayerFinishCurrentRoundResolution => playerFinishCurrentRoundResolution;

    public bool isAlive;
    public bool IsAlive => isAlive;


    public void Awake()
    {
        rigBonesManager = GetComponent<RigBonesManager>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerStatsController = GetComponent<PlayerStatsController>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerSpellInputController = GetComponent<PlayerSpellInputController>();
        playerSpellCaster = GetComponent<PlayerSpellController>();

        isAlive = true;
        playerFinishCurrentRoundResolution = false;

        Initialize();
        playerStatsController.Initialize(this);
        playerAnimationController.Initialize(this);
        playerSpellInputController.Initialize(this);
        playerSpellCaster.Initialize(this);
        GlobalEventsManager.Instance.roundEvents.onRoundCasting += ResetComponents;
    }
    public override void Initialize()
    {
        InitPlayerSkin();
        GroundPlayer();
    }
    public void ApplyLevelStatsModifier(PlayerStatSO levelStatsModifier)
    {
        playerStatsController.CurrentStats.ApplyModifier(levelStatsModifier.stats);
    }
    public void ApplyInventoryItemsStatModifiers(PlayerManager enemyPlayerManager)
    {
        foreach(ItemPreset itemPreset in customizedData.EquippedItemsPreset.AllItemPresets)
        {
            if(itemPreset.item != null)
            {
                playerStatsController.CurrentStats.ApplyModifier(itemPreset.item.PlayerStatModifier);
                enemyPlayerManager.PlayerStatsController.CurrentStats.ApplyModifier(itemPreset.item.EnemyStatModifier);
            }
        }
    }
    public void ApplyChangedPlayerStatsAccordingToNewStats()
    {
        playerStatsController.CurrentStats.currentHealth = playerStatsController.CurrentStats.maxHealth;
        playerStatsController.CurrentStats.currentMana = playerStatsController.CurrentStats.maxMana;
    }
    void InitPlayerSkin()
    {
        Transform rig = Instantiate(customizedData.PlayerSkin.SkinPrefab,rigRoot).transform;

        rig.localPosition = Vector3.zero;
        rig.localEulerAngles = Vector3.zero;
        Vector3 scale = Vector3.one;
        scale.x = customizedData.PlayerNum == 2 ? -1 : 1;
        rig.localScale = scale;
        rigBonesManager.Initialize(rig,customizedData.EquippedItemsPreset);

        playerAnimationController.Animator = rig.GetComponent<Animator>();

        playerAvatarImage.sprite = customizedData.PlayerSkin.AvatarSprite;
    }
    void GroundPlayer()
    {
        Ray ray = new Ray(transform.position,Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100f))
        {
            transform.position = hit.point;
        }
    }
    public void StartRound()
    {
        playerFinishCurrentRoundResolution = false;

    }
    public void FinishRoundResolution()
    {
        playerFinishCurrentRoundResolution = true;
        playerStatsController.RefillForNewRound();
    }
    public void ResetComponents()
    {
        playerAnimationController.Reset();
        playerSpellInputController.Reset();
        playerSpellCaster.Reset();
    }

    private void OnDestroy() 
    {
        GlobalEventsManager.Instance.roundEvents.onRoundCasting -= ResetComponents;
    }
}
