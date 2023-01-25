using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireSpellState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerFireSpellState(string id)
    {
        this.id = id;
    }
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.PlayerManager.PlayerAnimationController.CastSelectedSpell();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
