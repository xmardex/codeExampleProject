using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameStateBase
{
    private GameStateManager stateManager;
    public override void EnterState(GameStateManager stateManager)
    {
        this.stateManager = stateManager;
        RoundUIManager.Instance.StartPregameCountdown(CountdownToRoundStart);
    }
    
    public override void UpdateState(GameStateManager stateManager)
    {
        
    }
    void CountdownToRoundStart(int seconds)
    {
        if(seconds == Mathf.Ceil(stateManager.TimeBeforeGameStart/1.5f))
        {
            stateManager.P1_StateManager.Initialize();
            stateManager.P2_StateManager.Initialize();
        }
        if(seconds == 0)
        {
            stateManager.SwitchState(stateManager.RoundCastingState);
        }
    }
}
