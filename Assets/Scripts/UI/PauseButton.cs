using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManagment;
[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private RoundPauseMenu roundPauseMenu;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PauseGame);
    }
    void PauseGame()
    {
        bool isPause = GameManager.isGamePause;
        GameManager.Instance.PauseGame(!isPause);
        GlobalEventsManager.Instance.roundEvents.onGamePause?.Invoke(!isPause);
        roundPauseMenu.Activate(!isPause);
    }
}
