using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameStateBase
{
    PlayerManager playerManagerP1;
    PlayerManager playerManagerP2;
    PlayerStateManager stateManagerP1;
    PlayerStateManager stateManagerP2;
    public override void EnterState(GameStateManager stateManager)
    {
        stateManagerP1 = stateManager.P1_StateManager;
        stateManagerP2 = stateManager.P2_StateManager;
        playerManagerP1 = stateManagerP1.PlayerManager;
        playerManagerP2 = stateManagerP2.PlayerManager;
        
        bool isAIMode = playerManagerP2.IsAIControlled;
        int playerWinNum = 0;

        if(playerManagerP1.IsAlive)
        {
            stateManagerP1.SwitchState(stateManagerP1.WinState);
            stateManagerP2.SwitchState(stateManagerP2.LoseState);
            playerWinNum = playerManagerP1.PlayerNum;
        }
        
        if(playerManagerP2.IsAlive)
        {
            stateManagerP2.SwitchState(stateManagerP2.WinState);
            stateManagerP1.SwitchState(stateManagerP1.LoseState);
            playerWinNum = playerManagerP2.PlayerNum;
        }
        
        if(!playerManagerP1.IsAlive && !playerManagerP2.IsAlive)
        {
            stateManagerP1.SwitchState(stateManagerP1.LoseState);
            stateManagerP2.SwitchState(stateManagerP2.LoseState);
            //TODO: Game draw logic 
            playerWinNum = 0;
        }
                
        GlobalEventsManager.Instance.roundEvents.onGameOver?.Invoke(isAIMode,playerWinNum);
    }

    public override void UpdateState(GameStateManager stateManager)
    {
        
    }
}
