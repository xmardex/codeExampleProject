using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpellState : PlayerStateBase
{
    private string id;
    public override string ID { get => id; set => id = value;}
    public PlayerCastSpellState(string id)
    {
        this.id = id;
    }

    public override void EnterState(PlayerStateManager stateManager)
    {
        GlobalEventsManager.Instance.roundEvents.onRoundResolution?.Invoke();
        stateManager.PlayerManager.StartRound();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        
    }
}
