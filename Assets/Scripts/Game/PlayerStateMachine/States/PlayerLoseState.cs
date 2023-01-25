using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoseState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerLoseState(string id)
    {
        this.id = id;
    }
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.PlayerManager.PlayerAnimationController.Lose();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
