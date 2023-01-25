using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class BackButton : BaseScript
{
    private Button button;
    public Button Button => button;
    
    void Awake()
    {
        button = GetComponent<Button>();
        HistoryManager.Instance.SetBackButton(this);
    }
}
