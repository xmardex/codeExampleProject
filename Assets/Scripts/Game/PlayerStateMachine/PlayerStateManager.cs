using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerStateManager : MonoBehaviour
{
    
    [SerializeField]
    private PlayerManager playerManager;
    public PlayerManager PlayerManager => playerManager;
    private PlayerSpellInputController playerSpellInputController;
    public PlayerSpellInputController PlayerSpellInputController => playerSpellInputController;

    PlayerStateBase currentState;
    public PlayerStateBase CurrentState => currentState;

    private PlayerStateBase prepare = new PlayerPrepareState("prepare");
    public PlayerStateBase PrepareState => prepare;

    private PlayerStateBase castSpell = new PlayerCastSpellState("cast");
    public PlayerStateBase CastSpellState => castSpell;

    private PlayerStateBase fireSpell = new PlayerFireSpellState("fire");
    public PlayerStateBase FireSpellState => fireSpell;

    private PlayerStateBase hit = new PlayerHitState("hit");
    public PlayerStateBase HitState => hit;

    private PlayerStateBase lose = new PlayerLoseState("lose");
    public PlayerStateBase LoseState => lose;

    private PlayerStateBase win = new PlayerWinState("win");
    public PlayerStateBase WinState => win;

    private bool isInitialized = false;
    public bool IsInitialized => isInitialized;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerSpellInputController = GetComponent<PlayerSpellInputController>();
    }
    public void Initialize()
    {
        currentState = prepare;
        currentState.EnterState(this);
        isInitialized = true;
    }
    private void Update() 
    {
        currentState?.UpdateState(this);    
    }

    public void SwitchState(PlayerStateBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
