using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerCustomizeController : BaseScript
{
	[SerializeField]
	private Button selectButton;
	[SerializeField]
	private Button randomButton;
	[SerializeField]
	private PlayerCustomizedData playerData;
	[SerializeField]
	private CustomizerLocker locker;
	public CustomizerLocker Locker => locker;

	public PlayerCustomizedData PlayerData { get => playerData; }

	public Action onCustomizeFinished;
	public Action onCustomizeReset;
	public bool isFinished;

	private List<CustomizeItemSelector> allItemSelectors = new List<CustomizeItemSelector>();

	public override void Initialize()
	{
		allItemSelectors.AddRange(GetComponentsInChildren<CustomizeItemSelector>(true));
		int multiItemIndex = 0;
		foreach(CustomizeItemSelector itemSelector in allItemSelectors)
		{   
			itemSelector.ItemIndex = itemSelector.TargetItemType == ItemType.ring ? multiItemIndex++ : 0;
			itemSelector.PlayerNum = playerData.data.PlayerNum;
		}

		locker.Initialize();
		locker.SetLockerClickAction(() => LockCustomizer(false));
		selectButton.onClick.AddListener(() => {
			FinishCustomize(bySelectButton:true);
		});
		randomButton.onClick.AddListener(() => ChooseRandom());
		playerData.data.EquippedItemsPreset.ResetAllPresets();
	}
	private void OnEnable() 
	{
		if(playerData.data.PlayerNum == 1)
			GlobalEventsManager.Instance.menuEvents.onPlayer1CustomizationFinish += SwitchPlayerCustomizationUI;
			
		playerData.skinSelector.onSkinChanged += SkinChange;
	}
	public void SwitchPlayerCustomizationUI(bool isOn)
	{
		if(!isOn)
			EventsSystemHelper.SelectUIElement(selectButton.gameObject);
	}
	void SkinChange(SkinSO skin)
	{
		playerData.data.PlayerSkin = skin;
	}
	private void OnDisable()
	{
		if(playerData.data.PlayerNum == 1)
			GlobalEventsManager.Instance.menuEvents.onPlayer1CustomizationFinish -= SwitchPlayerCustomizationUI;
			
		playerData.skinSelector.onSkinChanged -= SkinChange;
	}
	public void ResetCustomizeData()
	{
		LockCustomizer(false);
		playerData.skinSelector.SetDefault();
		foreach(CustomizeItemSelector itemSelector in allItemSelectors)
				itemSelector.DeselectItem();
		onCustomizeReset?.Invoke();

		
	}
	public void LockCustomizer(bool isOn)
	{
		playerData.isSet = isOn;
		isFinished = isOn;
		locker.Activate(isOn);

		if(playerData.data.PlayerNum == 1)
			GlobalEventsManager.Instance.menuEvents.onPlayer1CustomizationFinish?.Invoke(isOn);
		if(playerData.data.PlayerNum == 2)
			GlobalEventsManager.Instance.menuEvents.onPlayer2CustomizationFinish?.Invoke(isOn);
	}
	public void FinishCustomize(bool setRandom = false, bool bySelectButton = false)
	{       
		if(setRandom)
		{
			ChooseRandom();
		}
		
		LockCustomizer(true);
				
		if(bySelectButton)
		{             
			if(isFinished)
				EventsSystemHelper.SelectUIElement(locker.LockButton.gameObject);
			else
				EventsSystemHelper.SelectUIElement(selectButton.gameObject);
		}
		
		onCustomizeFinished?.Invoke();

		
	}
	public void ChooseRandom()
	{
		playerData.Random();
			foreach(CustomizeItemSelector itemSelector in allItemSelectors)
				itemSelector.SelectItemRandom();
	}
	
}
[Serializable]
public class PlayerCustomizedData
{
	public bool isSet = false;
	public SkinSelector skinSelector;
	public PlayerCustomizeDataSO data;

	public void Random()
	{
		//Set random data
		skinSelector.RandomSkin();
	}
}
