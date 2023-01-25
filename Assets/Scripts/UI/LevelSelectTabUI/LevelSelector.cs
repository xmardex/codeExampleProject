using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagment;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{

    [SerializeField]
    private Image levelPreviewImage;
    [SerializeField]
    private GameObject levelButtonPrefab;
    [SerializeField]
    private Transform levelButtonsRoot;
    [SerializeField]
    private LevelHolderSO levelHolderSO;
    private List<LevelButton> levelButtons = new List<LevelButton>();
    [SerializeField]
    private ListButtonSelector listButtonSelector;
    public Button firstButton;
    private void Awake() 
    {
        foreach(LevelSO levelSO in levelHolderSO.DuelLevelsList)
        {
            LevelButton levelButton = Instantiate(levelButtonPrefab,levelButtonsRoot).GetComponent<LevelButton>();
            levelButton.Initialize(levelSO);
            levelButton.onLevelSelect += OnSelectedLevelChange;
            levelButtons.Add(levelButton);
        }      
        if(levelButtons.Count == 0){Debug.LogError("Must be at least one level", this); return;}

        listButtonSelector.DefaultButton = levelButtons[0].GetComponent<Button>();
        listButtonSelector.Initialize();
        firstButton = levelButtons[0].Button;
        SetupLevelBtnsNavigation();
    }
    void OnEnable()
    {
        EventsSystemHelper.SelectUIElement(firstButton.gameObject);
    }
    public void OnSelectedLevelChange(LevelSO selectedLevel)
    {
        GameManager.Instance.SelectedLevel = selectedLevel;
        levelPreviewImage.sprite = selectedLevel.LevelPreview;
    }
    private void OnDestroy() {
        foreach(LevelButton levelBtn in levelButtons)
        {
            levelBtn.onLevelSelect -= OnSelectedLevelChange;
        }  
    }
    void SetupLevelBtnsNavigation()
    {
        List<Selectable> allSelectable = new List<Selectable>();

        foreach(LevelButton btn in levelButtons)
            allSelectable.Add(btn.GetComponent<Selectable>()); 
        
        for (int i = 0; i < allSelectable.Count; i++)
        {   
            Selectable selectable = allSelectable[i];
            
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;


            nav.selectOnUp = HistoryManager.Instance.BackButton.Button;
            nav.selectOnLeft = i-1 >= 0 ? allSelectable[i-1] : null;
            nav.selectOnRight = i+1 < allSelectable.Count ? allSelectable[i+1] : null;
            nav.selectOnDown = StartGameButton.activeStartGameButton;


            selectable.navigation = nav;
        }

        Navigation cycleNavFirst = allSelectable[0].navigation;
        cycleNavFirst.mode = Navigation.Mode.Explicit;
        Navigation cycleNavLast = allSelectable[allSelectable.Count-1].navigation;
        cycleNavLast.mode = Navigation.Mode.Explicit;

        cycleNavFirst.selectOnLeft = allSelectable[allSelectable.Count-1];
        cycleNavLast.selectOnRight = allSelectable[0];

        allSelectable[allSelectable.Count-1].navigation = cycleNavLast;
        allSelectable[0].navigation = cycleNavFirst;

        //BackButtonNav

        Navigation startGameBtn = StartGameButton.activeStartGameButton.navigation;
        startGameBtn.mode = Navigation.Mode.Explicit;
        startGameBtn.selectOnUp = allSelectable[0];
        StartGameButton.activeStartGameButton.navigation = startGameBtn;

        Navigation backBtnNavigation = HistoryManager.Instance.BackButton.Button.navigation;
        backBtnNavigation.selectOnDown = allSelectable[0];
        HistoryManager.Instance.BackButton.Button.navigation = backBtnNavigation;

    }
}
