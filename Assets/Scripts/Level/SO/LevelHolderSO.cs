using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelsHolder", menuName = "GameLevels/LevelSOHolder", order = 0)]
public class LevelHolderSO : ScriptableObject {
    [SerializeField]
    private List<LevelSO> duelModeLevels = new List<LevelSO>();
    public List<LevelSO> DuelLevelsList {get => duelModeLevels;}

    [SerializeField]
    private List<LevelSO> storyModeLevels = new List<LevelSO>();
    public List<LevelSO> StoryLevelsList {get => storyModeLevels;}

}