using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagment;
public class GameStateManager : Singleton<GameStateManager>
{   
    GameStateBase currentState;

    private GameStateBase startGame = new StartGameState();
    public GameStateBase StartGameState => startGame;

    private GameStateBase roundCasting = new RoundCastingState();
    public GameStateBase RoundCastingState => roundCasting;

    private GameStateBase roundResolution = new RoundResolutionState();
    public GameStateBase RoundResolutionState => roundResolution;

    private GameStateBase endGame = new EndGameState();
    public GameStateBase EndGame => endGame;
    
    public PlayerStateManager P1_StateManager => LevelManager.Instance.Player1.PlayerStateManager;
    public PlayerStateManager P2_StateManager => LevelManager.Instance.Player2.PlayerStateManager;

    private int roundNum = 0;
    public int RoundNum{get => roundNum; set => roundNum = value;}
    
    public int RoundDuration => GameManager.Instance.GameSettings.RoundSettings.roundDuration;

    public int TimeBeforeGameStart => GameManager.Instance.GameSettings.RoundSettings.timeBeforeGameStart;

    public void Initialize()
    {
        currentState = startGame;
        currentState.EnterState(this);
    }
    private void Update() 
    {
        currentState?.UpdateState(this);
    }

    public void SwitchState(GameStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
