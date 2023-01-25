using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrepareState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerPrepareState(string id)
    {
        this.id = id;
    }
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.PlayerSpellInputController.isInputAllowed = false;
        stateManager.PlayerManager.PlayerAnimationController.Greeting();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
