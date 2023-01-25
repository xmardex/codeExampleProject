using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPauseMenu : BaseScript
{
    [SerializeField]
    private GameObject defaultSelectedButton;

    public override void Activate(bool isOn)
    {
        base.Activate(isOn);
        if(isOn)
        EventsSystemHelper.SelectUIElement(defaultSelectedButton);
    }
}
