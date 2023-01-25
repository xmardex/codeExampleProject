using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Button))]
public class UINavigationSwitcher : MonoBehaviour
{
	private Button button;
	private Navigation defNavigation;
	[SerializeField]
	private NavigationUIPresetsList navigationUIPresetsList;
	private void Awake() 
	{
		button = GetComponent<Button>();
		foreach(NavigationUIPreset navigationUIPreset in navigationUIPresetsList.navigationUIPresets)
		{
			if(navigationUIPreset.actionToSwitch.actionName == "none") return;

			if(GlobalEventsManager.Instance.menuEvents.CheckActionType(navigationUIPreset.actionToSwitch.actionName,typeof(ScreenUI)))
			{
				GlobalEventsManager.Instance.menuEvents.onScreenSwitch += OnScreenUIEvent;
			}
			if(GlobalEventsManager.Instance.menuEvents.CheckActionType(navigationUIPreset.actionToSwitch.actionName,typeof(bool)))
			{
				GlobalEventsManager.Instance.menuEvents.onPlayer1CustomizationFinish += OnBoolEvent;
			}
		}

	}
	void OnDestroy()
	{
		GlobalEventsManager.Instance.menuEvents.onScreenSwitch -= OnScreenUIEvent;
		GlobalEventsManager.Instance.menuEvents.onPlayer1CustomizationFinish -= OnBoolEvent;
	}
	void OnBoolEvent(bool isOn)
	{
		if(isOn)
			SwitchNavigation("player2");
		else
			SwitchNavigation("player1");
	}

	private void SwitchNavigation(string navPresetID)
	{
		if(button != null)
			button.navigation = navigationUIPresetsList.navigationUIPresets.Find(nav=>nav.presetID == navPresetID).navigation;
	}
	void OnScreenUIEvent(ScreenUI screenUI)
	{
		switch(screenUI.ScreenType)
		{
			case ScreenType.CustomizeWizards:
			{
				SwitchNavigation("customization"); 
			}
			break;
		}
	}
}
[Serializable]
public class UINavigationPreset
{
	public string id;
	public Navigation altNavigation;

}
