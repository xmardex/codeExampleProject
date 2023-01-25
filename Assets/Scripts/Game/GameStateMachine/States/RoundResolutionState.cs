using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundResolutionState : GameStateBase
{
    PlayerStateManager stateManagerP1;
    PlayerStateManager stateManagerP2;
    public override void EnterState(GameStateManager stateManager)
    {
        stateManagerP1 = stateManager.P1_StateManager;
        stateManagerP2 = stateManager.P2_StateManager;

        SpellSO spellP1 = stateManagerP1.PlayerManager.PlayerSpellCaster.CurrentSpell;
        SpellSO spellP2 = stateManagerP2.PlayerManager.PlayerSpellCaster.CurrentSpell;

        LevelManager.Instance.ApplyBuffs(spellP1,spellP2);

        GlobalEventsManager.Instance.roundEvents.onRoundResolution?.Invoke();

        stateManager.P1_StateManager.SwitchState(stateManagerP1.FireSpellState);
        stateManager.P2_StateManager.SwitchState(stateManagerP2.FireSpellState);
        
    }

    public override void UpdateState(GameStateManager stateManager)
    {
        bool isFinishResolutionP1 = stateManagerP1.PlayerManager.PlayerFinishCurrentRoundResolution;
        bool isFinishResolutionP2 = stateManagerP2.PlayerManager.PlayerFinishCurrentRoundResolution;

        if(isFinishResolutionP1 && isFinishResolutionP2)
        {
            if(stateManagerP1.PlayerManager.IsAlive && stateManagerP2.PlayerManager.IsAlive)
            {
                stateManager.SwitchState(stateManager.RoundCastingState);
            }
            else
                stateManager.SwitchState(stateManager.EndGame);
        }
    }
}
