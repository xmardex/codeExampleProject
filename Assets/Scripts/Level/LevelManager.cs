using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagment;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private LevelType levelType;
    private PlayerStatSO levelPlayerStatModifier;

    [SerializeField]
    private PlayerManager player1;
    public PlayerManager Player1 => player1;

    [SerializeField]
    private PlayerManager player2;
    public PlayerManager Player2 => player2;
    
    public void LevelInitialize()
    {
        Initialize();
        RoundUIManager.Instance.Initialize();
        GameStateManager.Instance.Initialize();
    }

    public RoundDamages GetRoundDamages()
    {
        CastedSpellData spellDataP1 = player1.PlayerSpellInputController.CastedSpellData;
        CastedSpellData spellDateP2 = player2.PlayerSpellInputController.CastedSpellData;
        
        return CoreCalculation.CalculateSpellDamage(spellDataP1.manaChanneled,spellDateP2.manaChanneled,spellDataP1.spell,spellDateP2.spell);
    }
    public void ApplyBuffs(SpellSO spellP1, SpellSO spellP2)
    {
        foreach(BuffEffect buffEffect in spellP1.BuffEffects)
            if(buffEffect.BuffCondition.Target == BuffTarget.self)
                player1.PlayerStatsController.AddBuff(buffEffect);
            else if (buffEffect.BuffCondition.Target == BuffTarget.enemy)
                player2.PlayerStatsController.AddBuff(buffEffect);
            else
            {
                player1.PlayerStatsController.AddBuff(buffEffect);
                player2.PlayerStatsController.AddBuff(buffEffect);
            }

        foreach(BuffEffect buffEffect in spellP2.BuffEffects)
            if(buffEffect.BuffCondition.Target == BuffTarget.self)
                player2.PlayerStatsController.AddBuff(buffEffect);
            else if (buffEffect.BuffCondition.Target == BuffTarget.enemy)
                player1.PlayerStatsController.AddBuff(buffEffect);
            else
            {
                player1.PlayerStatsController.AddBuff(buffEffect);
                player2.PlayerStatsController.AddBuff(buffEffect);
            }
    }
    void Initialize()
    {
        levelPlayerStatModifier = GameManager.Instance.SelectedLevel.LevelStatsModifier;
        
        InitializePlayer(player1);
        InitializePlayer(player2);

        player1.opponentPlayer = player2;
        player2.opponentPlayer = player1;

        player2.IsAIControlled = GameManager.Instance.GameMode == GameMode.ai;  
    }
    void InitializePlayer(PlayerManager player)
    {
        player.ApplyLevelStatsModifier(levelPlayerStatModifier);
        player.ApplyInventoryItemsStatModifiers(player == player1 ? player2 : player1);
        player.ApplyChangedPlayerStatsAccordingToNewStats();
    }
}
