using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
public class GlobalEventsManager : Singleton<GlobalEventsManager>
{
	[SerializeField][HideInInspector]
	public MenuEvents menuEvents = new MenuEvents();
	[SerializeField][HideInInspector]
	public RoundEvents roundEvents = new RoundEvents();
	void Awake()
	{
		base.SetAsCrossScene();
	}
}
[Serializable]
public class RoundEvents : EventList<RoundEvents>
{
	//WARNING! Add new to the bottom only!
	public Action<bool> onGamePause;
	public Action onRoundCasting;
	public Action onRoundResolution;
	public Action onRoundEnd;
	public Action<bool,int> onGameOver;
	// ^ new here ^
}

[Serializable]
public class MenuEvents : EventList<MenuEvents>
{
	//WARNING! Add new to the bottom only!
	public Action<ScreenUI> onScreenSwitch;
	public Action<bool> onCustomizationInventoryActivate;
	public Action<bool> onPlayer1CustomizationFinish;
	public Action<bool> onPlayer2CustomizationFinish;
	// ^ new here ^
}

[Serializable]
public class EventList<T>
{
	//Use those methods only if there is no other way
	public List<string> ActionsListNames
	{
		get 
		{
			List<string> actions = new List<string>();
			actions.Add("none");
			FieldInfo[] fields = typeof(T).GetFields();
			foreach(FieldInfo fieldInfo in fields){
				actions.Add(fieldInfo.Name);
			}
			return actions;      
		}
	}
	public bool CheckActionType(string fieldName, Type targetType)
	{
		Type fieldType = typeof(T).GetField(fieldName).FieldType;
		if(fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Action<>))
		{
			Type itemType = fieldType.GetGenericArguments()[0];
			if(itemType == targetType)
			{
				return true;
			} 
		}
		return false;
	}
}
[Serializable]
public class MenuEventsDropdown
{
	[SerializeField][Dropdown("GlobalEventsManager.menuEvents.ActionsListNames")]
	public string actionName; 
}
[Serializable]
public class RoundEventsDropdown
{
	[SerializeField][Dropdown("GlobalEventsManager.roundEvents.ActionsListNames")]
	public string actionName; 
}