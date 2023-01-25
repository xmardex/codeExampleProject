using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
public class CustomizerLocker : BaseScript
{
    [SerializeField]
    private TMP_Text lockText;

    [SerializeField]
    private string aiReadyText;
    [SerializeField]
    private string playerReadyText;
    private Button button;
    public Button LockButton => button;

    public override void Initialize()
    {
        button = GetComponent<Button>();
    }

    public void SetLockerClickAction(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    public void SetAsAI()
    {
        button.enabled = false;
        lockText.text = aiReadyText;
    }
    public void SetAsPlayer()
    {
        button.enabled = true;
        lockText.text = playerReadyText;
    }
}
