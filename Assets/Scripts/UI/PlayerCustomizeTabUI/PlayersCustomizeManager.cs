using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagment;
using UnityEngine.UI;
public class PlayersCustomizeManager : BaseScript
{
    [SerializeField]
    private PlayerCustomizeController playerCustomizeController1;
    [SerializeField]
    private PlayerCustomizeController playerCustomizeController2;

    [SerializeField]
    private ListButtonSelector gameModeButtonsList;

    private GameMode mode;

    public void Start()
    {
        Initialize();
        gameModeButtonsList.Initialize();
        //SetDefault Mode
        GameManager.Instance.ChangeGameMode(GameMode.ai);
        
    }
    public override void Initialize()
    {
        playerCustomizeController1.Initialize();
        playerCustomizeController2.Initialize();
        playerCustomizeController1.onCustomizeFinished += CustomizeFinished;
        playerCustomizeController2.onCustomizeFinished += CustomizeFinished;
        GameManager.Instance.onGameModeChange += GameModeChanged;
    }
    void GameModeChanged(GameMode mode)
    {
        this.mode = mode;
        switch(mode)
        {
            case GameMode.ai:
            {
                playerCustomizeController2.FinishCustomize(true);
                playerCustomizeController2.Locker.Activate(true);
                playerCustomizeController2.Locker.SetAsAI();
            }
            break;
            case GameMode.pvp:
            {
                playerCustomizeController2.Locker.Activate(false);
                playerCustomizeController2.ResetCustomizeData();
                playerCustomizeController2.Locker.SetAsPlayer();
            }
            break;

            default:
            Debug.LogError("mode changed to null",this);
            return;
        }
    }
    void UnlockCustomizers()
    {
        switch(mode)
        {
            case GameMode.ai:
            {
                playerCustomizeController1.LockCustomizer(false);
                
            }
            break;
            case GameMode.pvp:
            {
                playerCustomizeController1.LockCustomizer(false);
                playerCustomizeController2.LockCustomizer(false);
            }
            break;

            default:
            Debug.LogError("mode changed to null",this);
            return;
        }
    }
    void CustomizeFinished()
    {
        if(playerCustomizeController1.isFinished && playerCustomizeController2.isFinished)
        {
            MainMenuController.Instance.SwitchTo(ScreenType.SelectLevel);
            HistoryManager.Instance.AddSubActionToLast(UnlockCustomizers);
        }
    }
    void OnDestroy()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.onGameModeChange -= GameModeChanged;
        playerCustomizeController1.onCustomizeFinished -= CustomizeFinished;
        playerCustomizeController2.onCustomizeFinished -= CustomizeFinished;
    }
}
