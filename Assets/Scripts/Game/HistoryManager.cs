using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HistoryManager : Singleton<HistoryManager>
{
	private Stack<HistoryAction> history = new Stack<HistoryAction>();
	private BackButton backButton;
	public BackButton BackButton => backButton;
	private HistoryAction prevAction = null;
	
	public static GameObject prevEventsSystemSelectedGameObject;

	private void Awake() 
	{
		base.SetAsCrossScene();
	}
	
	public void SetBackButton(BackButton backBtn)
	{
		if(backBtn == null)
		{
			Debug.LogError("BackButton is null",this); 
			return;
		}

		backButton?.Button.onClick.RemoveAllListeners();
		backButton = backBtn;
		backButton.Button.onClick.AddListener(Back);
		
		if(history.Count == 0)
			backBtn.Activate(false);
	}
	public void AddBackAction(Action action, Action subAction = null)
	{
		HistoryAction historyAction = new HistoryAction(action,subAction);
		history.Push(historyAction);
		backButton.Activate(true);
	}
	public void AddSubActionToLast(Action subAction)
	{
		HistoryAction backAction = history.Peek();
		backAction.subAction = subAction;
	}
	public void ClearActionsList()
	{
		history.Clear();
	}
	public void Back()
	{
		HistoryAction backAction = history.Pop();
		prevAction = backAction;
		backAction.action?.Invoke();
		backAction.subAction?.Invoke();

		if(history.Count == 0)
		{
			backButton.Activate(false);
		}
	}
	void OnDestroy()
	{
		backButton?.Button?.onClick.RemoveAllListeners();
	}
}
[Serializable]
public class HistoryAction
{
	public Action action;
	public Action subAction;

	public HistoryAction(Action action, Action subAction = null)
	{
		this.action = action;
		this.subAction = subAction;
	}
}
