using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GlobalSettingsSO", menuName = "RoundData/GameSettingsSO", order = 0)]
public class GameSettingsSO : ScriptableObject 
{
    [SerializeField]
    private InputTiming inputTiming;
    public InputTiming InputTiming => inputTiming;
    [SerializeField]
    private SpellCastValues spellCastValues;
    public SpellCastValues SpellCastValues => spellCastValues;
    [SerializeField]
    private AISpellCast aiSpellCast;
    public AISpellCast AISpellCast => aiSpellCast;
    [SerializeField]
    private RoundSettings roundSettings;
    public RoundSettings RoundSettings => roundSettings;
    [SerializeField]
    private SpellCalculationValues spellCalculationValues;
    public SpellCalculationValues SpellCalculationValues => spellCalculationValues;

    [SerializeField]
    private Vector3 playerPositionOffset;
    public Vector3 PlayerPositionOffset => playerPositionOffset;
}
[Serializable]
public class InputTiming
{
    public float timeToStartManaChanneling;
    public float timeToResetShapeSelectionIfThereIsNothingSelected;
}
[Serializable]
public class SpellCastValues
{
    public float defaultSpellSize;
    public float spellIncreaseSpeed;
    public float spellFireSpeed;
    public float spellTargetMinDistance;
}
[Serializable]
public class AISpellCast
{
    public float minManaChannellingTime;

    [Header("Must be less or equals to round time duration")]
    public float maxManaChannellingTime; 
}
[Serializable]
public class RoundSettings
{
    public int roundDuration = 5;
    public int timeBeforeGameStart = 5;
}
[Serializable]
public class SpellCalculationValues
{
    public float powerDivisor = 3;
    public float resistK = 0.3f;
    public float spellModeBonusK = 0.3f;
}