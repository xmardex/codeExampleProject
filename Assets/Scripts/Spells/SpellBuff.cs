using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpellBuff
{
    [SerializeField]
    private BuffEffect buffEffect;
    private PlayerStatsController playerStats;
    [SerializeField]
    private int buffDuration;
    [SerializeField]
    private bool isApplied;
    [SerializeField]
    private bool perRound;

    public SpellBuff(BuffEffect buffEffect, PlayerStatsController playerStats)
    {
        this.buffEffect = buffEffect;
        this.playerStats = playerStats;
        this.buffDuration = buffEffect.BuffCondition.RoundDuration;
        this.perRound = 
            buffEffect.BuffCondition.Operation == BuffOperation.gainPerRound || 
            buffEffect.BuffCondition.Operation == BuffOperation.losePerRound;
    }
    void ApplyBuffEffectToStats()
    {
        playerStats.CurrentStats.SpellBuffHandle(buffEffect,true);
        Debug.Log(
@$"Player{playerStats.PlayerManager.PlayerNum} buff process: {buffEffect.BuffName} 
Rounds left: {buffDuration}
"
        );
    }
    void RemoveBuffEffectFromStats()
    {
        Debug.Log(
@$"Player{playerStats.PlayerManager.PlayerNum} buff removed: {buffEffect.BuffName}
"
        );
        playerStats.CurrentStats.SpellBuffHandle(buffEffect,false);
    }
    public void ProcessBuff()
    {
        if(!perRound && !isApplied)
        {
            ApplyBuffEffectToStats();
            isApplied = true;
        }

        if(buffDuration > 0)
        {
            if(perRound)
            {
                ApplyBuffEffectToStats();
            }
            buffDuration--;
        }
        else
        {   
            if(!perRound)
            {
                RemoveBuffEffectFromStats();
            }
            playerStats.RemoveBuff(this);
        }
    }
}
