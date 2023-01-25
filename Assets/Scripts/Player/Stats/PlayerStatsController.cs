using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using NaughtyAttributes;
using GameManagment;
public class PlayerStatsController : BaseScript
{
    [SerializeField]
    private PlayerStatSO baseStats;
    [SerializeField]
    private PlayerStats currentStats;
    public PlayerStats CurrentStats => currentStats;

    [SerializeField]
    private List<SpellBuff> currentPlayerBuffs = new List<SpellBuff>();
    public List<SpellBuff> CurrentPlayerBuffs => currentPlayerBuffs;


    [SerializeField]
    private PlayerUIReferences playerUIReferences;

    [SerializeField,ReadOnly]
    private bool isManaChannelling;
    [SerializeField,ReadOnly]
    private bool doChannelling;
    private ElementStat currentElementStats;

    PlayerManager playerManager;
    public PlayerManager PlayerManager => playerManager;
    PlayerSpellInputController playerSpellInputController;

    public void Initialize(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        playerSpellInputController = playerManager.PlayerSpellInputController;

        currentStats = baseStats.stats.Clone();

        GlobalEventsManager.Instance.roundEvents.onRoundCasting += ResetValues;
    }

    //calculate stats maybe call this in Update

    void Update()
    {
        ProcessStats();
    }
    void ProcessStats()
    {
        ProcessManaChannelling();
        UpdateStatsUI();
    }
    void ProcessManaChannelling()
    {
        doChannelling = isManaChannelling && currentStats.currentMana > 0;

        if(doChannelling)
        {
            playerSpellInputController.manaChanneled += Time.deltaTime*currentElementStats.chargeRate;
            currentStats.currentMana -= Time.deltaTime*currentElementStats.chargeRate;
        }

        if(playerSpellInputController.manaChanneled > 0 && !doChannelling && currentStats.currentMana <= currentStats.maxMana)
        {
            playerSpellInputController.manaChanneled -= Time.deltaTime*currentElementStats.windDownRate;
            currentStats.currentMana += Time.deltaTime*currentElementStats.windDownRate;
        }

        if(playerSpellInputController.manaChanneled < 0 && !doChannelling)
            playerSpellInputController.manaChanneled = 0;
            
        currentStats.currentMana = Math.Clamp(currentStats.currentMana,0,currentStats.maxMana);
    }
    public void StartManaChannelling(SpellInputData spellInputData)
    {   
        ApplySpellInput(spellInputData);

        if(!isManaChannelling)
            Timer.StartSimpleFloatTimer("startManaChanneling",GameManager.Instance.GameSettings.InputTiming.timeToStartManaChanneling,delegate
            {
                isManaChannelling = true;
            });
    }
    public void ApplySpellInput(SpellInputData spellInputData)
    {
        ElementStat elementStat_1 = currentStats.GetElementStat(spellInputData.Element_1.elementRawType);
        ElementStat elementStat_2 = currentStats.GetElementStat(spellInputData.Element_2.elementRawType);
        
        currentElementStats = elementStat_1.chargeRate >= elementStat_2.chargeRate ?
            elementStat_1 : elementStat_2;
    }
    public void StopManaChannelling()
    {
        Timer.StopSimpleTimerFloatInstance("startManaChanneling");
        isManaChannelling = false;
    }
    public void AIStartManaChannelling(float timeToChannelling, SpellInputData spellInputData)
    {
        ApplySpellInput(spellInputData);

        isManaChannelling = true;
        Timer.StartSimpleFloatTimer("AIManaChanneling",timeToChannelling,delegate
        {
            isManaChannelling = false;
        ;});
    }
    public void GetDamage()
    {
        RoundDamages roundDamages = LevelManager.Instance.GetRoundDamages();
        switch(playerManager.PlayerNum)
        {
            case 1:
            {
                currentStats.currentHealth -= roundDamages.P1_Damage;
            }
            break;
            case 2:
            {
                currentStats.currentHealth -= roundDamages.P2_Damage;
            }
            break;
        }

        if(currentStats.currentHealth < 0)
            currentStats.currentHealth = 0;
        playerManager.isAlive = CheckPlayerAlive();
    }
    bool CheckPlayerAlive()
    {
        return currentStats.currentHealth > 0;
    }
    void ResetValues()
    {
        currentElementStats = null;
        playerSpellInputController.manaChanneled = 0;
        isManaChannelling = false;
    }
    public void RefillForNewRound()
    {
        currentStats.currentMana += currentStats.manaReplenishRate;
        //TODO: increment buffs duration by 1 

        foreach(SpellBuff spellBuff in currentPlayerBuffs.ToArray())
            spellBuff.ProcessBuff();

    }
    public void AddBuff(BuffEffect buffEffect)
    {
        currentPlayerBuffs.Add(new SpellBuff(buffEffect,this));
    }
    public void RemoveBuff(SpellBuff buff)
    {
        currentPlayerBuffs.Remove(buff);
    }
    void UpdateStatsUI()
    {
        float hpValue = currentStats.currentHealth/currentStats.maxHealth;
        playerUIReferences.healthBar.value = hpValue;

        float manaValue = currentStats.currentMana/currentStats.maxMana;
        playerUIReferences.manaBar.value = manaValue;
    }

    private void OnDestroy() {
        GlobalEventsManager.Instance.roundEvents.onRoundCasting -= ResetValues;
    }

}

[Serializable]
public class PlayerUIReferences
{
    [SerializeField]
    public Slider healthBar;
    [SerializeField]
    public Slider manaBar;
}