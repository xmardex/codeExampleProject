using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "_levelData", menuName = "GameLevels/LevelDataSO", order = 0)]
public class LevelSO : ScriptableObject {

    [SerializeField]
    private string levelName;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private Sprite levelPreview;
    [SerializeField]
    private PlayerStatSO levelStatsModifier;

    public Sprite LevelPreview {get => levelPreview;}
    public string SceneName { get => sceneName;}
    public string LevelName { get => levelName;}
    public PlayerStatSO LevelStatsModifier { get => levelStatsModifier;}
}