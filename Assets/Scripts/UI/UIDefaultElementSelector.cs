using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[ExecuteInEditMode]
public class UIDefaultElementSelector : MonoBehaviour
{
    [SerializeField]
    private Selectable defaultSelectable;
    
    public void Select()
    {
        EventsSystemHelper.SelectUIElement(defaultSelectable.gameObject);
    }
}
