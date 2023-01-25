using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScreenUI : BaseScript
{
    [SerializeField]
    private Button defaultSelectedButton;

    [Header("WARNING: This should be on root screen panel gameObject")]
    [Header("---------------")]
    [SerializeField]
    private bool isOverlay;
    public bool IsOverlay => isOverlay;
    [Header("Leave Switch Button empty if this is first screen or its open other way")]
    [SerializeField]
    private Button switchButton;
    [SerializeField]
    private ScreenType type;
    public ScreenType ScreenType => type;

    public override void Initialize()
    {
        switchButton?.onClick.AddListener(Switch);
        Hide();
    }
    public void Show()
    {
        Activate(true);

        if(defaultSelectedButton != null)
            EventsSystemHelper.SelectUIElement(defaultSelectedButton.gameObject);
    }
    public void Hide()
    {
        Activate(false);
    }
    public void Switch()
    {
        MainMenuController.Instance.SwitchTo(this);
        
    }
}
