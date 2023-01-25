using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManagment;
[RequireComponent(typeof(Button))]
public class GameModeButton : MonoBehaviour
{
    [SerializeField]
    private GameMode mode;
    private Button button;
    private void Awake() 
    {
        button = GetComponent<Button>();
    }
    private void Start() 
    {
        button.onClick.AddListener(delegate{ChangeGameMode(mode);});
    }
    void ChangeGameMode(GameMode mode)
    {
        GameManager.Instance.ChangeGameMode(mode);
    }
    private void OnDestroy() {
        button.onClick.RemoveAllListeners();
    } 
}
