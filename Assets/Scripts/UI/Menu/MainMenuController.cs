using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainMenuController : Singleton<MainMenuController>
{
    private List<ScreenUI> _allScreens = new List<ScreenUI>();
    [SerializeField]
    private ScreenUI currentScreen;
    public ScreenUI CurrentScreen;
    HistoryManager historyManager => HistoryManager.Instance;
    [SerializeField]
    private ScreenUI firstScreen;
    private void Awake() 
    {
        _allScreens.AddRange(FindObjectsOfType<ScreenUI>(true));
        foreach(ScreenUI screenUI in _allScreens)
        {
            screenUI.Initialize();
        }
        SwitchTo(firstScreen);
    }
    public void SwitchTo(ScreenUI screenUI)
    {

        if(currentScreen != null && screenUI != firstScreen)
        {
            ScreenUI prevScreen = currentScreen;
            historyManager.AddBackAction(delegate{Back(prevScreen);});
        }


        if(!screenUI.IsOverlay)
        currentScreen?.Hide();
        
        screenUI.Show();
        currentScreen = screenUI;
        
        GlobalEventsManager.Instance.menuEvents.onScreenSwitch?.Invoke(screenUI);
    }
    public void SwitchTo(ScreenType screenType)
    {
        ScreenUI screenUI = _allScreens.Find(s=>s.ScreenType == screenType);
        SwitchTo(screenUI);
    }
    private void Back(ScreenUI screenUI)
    {
        if(!screenUI.IsOverlay)
        currentScreen?.Hide();
        
        screenUI.Show();
        currentScreen = screenUI;
        
        GlobalEventsManager.Instance.menuEvents.onScreenSwitch?.Invoke(screenUI);
    }

}
