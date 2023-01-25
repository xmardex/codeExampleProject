using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManagment;
[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    private Button button;
    public static Button activeStartGameButton;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
        activeStartGameButton = button;
    }
    public void StartGame()
    {
        GameManager.Instance.StartGameLevel();
    }
}
