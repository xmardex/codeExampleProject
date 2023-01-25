using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCastingState : GameStateBase
{   
    private GameStateManager stateManager;
    PlayerStateManager playerStateManagerP1;
    PlayerStateManager playerStateManagerP2;
    public override void EnterState(GameStateManager stateManager)
    {
        this.stateManager = stateManager;
        playerStateManagerP1 = stateManager.P1_StateManager;
        playerStateManagerP2 = stateManager.P2_StateManager;

        stateManager.RoundNum++;
        RoundUIManager.Instance.StartNewRound(stateManager.RoundNum,stateManager.RoundDuration,() => RoundTimerEnd());

        playerStateManagerP1.SwitchState(playerStateManagerP1.CastSpellState);
        playerStateManagerP2.SwitchState(playerStateManagerP2.CastSpellState);
        
        GlobalEventsManager.Instance.roundEvents.onRoundCasting?.Invoke();
    }
    void RoundTimerEnd()
    {
        stateManager.SwitchState(stateManager.RoundResolutionState);
    }
    public override void UpdateState(GameStateManager stateManager)
    {
        
    }
}
