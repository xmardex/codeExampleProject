using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelNameText;
    private LevelSO levelSO;
    public LevelSO LevelDataSO {set => levelSO = value;}
    public Action<LevelSO> onLevelSelect;
    private Button button;
    public Button Button => button;
    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectLevel);
    }
    void OnDestroy()
    {
        button.onClick.RemoveListener(SelectLevel);
    }
    public void Initialize(LevelSO levelSO)
    {
        this.levelSO = levelSO;
        levelNameText.text = levelSO.LevelName;
    }
    public void SelectLevel()
    {
        onLevelSelect?.Invoke(levelSO);
    }
}
