using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerWinState(string id)
    {
        this.id = id;
    }
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.PlayerManager.PlayerAnimationController.Win();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
