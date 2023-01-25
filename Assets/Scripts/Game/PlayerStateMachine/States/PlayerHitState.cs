using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerHitState(string id)
    {
        this.id = id;
    }
    public override void EnterState(PlayerStateManager stateManager)
    {
        
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
