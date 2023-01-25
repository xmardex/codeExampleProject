using System;
using System.Collections.Generic;
using UnityEngine.UI;
[Serializable]
public class NavigationUIPresetsList
{
    public List<NavigationUIPreset> navigationUIPresets = new List<NavigationUIPreset>();
}
[Serializable]
public class NavigationUIPreset
{
    public string presetID;
    public MenuEventsDropdown actionToSwitch;
    public Navigation navigation;
}
