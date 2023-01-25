using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(fileName = "_spellShape", menuName = "Spells/SpellShapeSO", order = 0)]
public class SpellShapeSO : ScriptableObject 
{
	
	[SerializeField,Header("Don't change type of shape"),OnValueChanged("UpdateShapeModeType")]
	private SpellShapeType spellShapeType;
	public SpellShapeType SpellShapeType => spellShapeType;

	[SerializeField,ReadOnly]
	private SpellShapeModeType spellShapeMode;
	public SpellShapeModeType SpellShapeMode => spellShapeMode;

	[SerializeField]
	private GameObject shapePrefab;
	public GameObject ShapePrefab => shapePrefab;
	
	[SerializeField]
	private StatsDamage statsDamage;
	public StatsDamage StatsDamage => statsDamage;
	
	[SerializeField]
	private StatsResist statsResist;
	public StatsResist StatsResist => statsResist;
	
	[Button("Update shape mode type")]
	void UpdateShapeModeType()
	{
		switch (spellShapeType)
		{
			case SpellShapeType.bolt:
				spellShapeMode = SpellShapeModeType.Offensive;
			break;
			case SpellShapeType.barrage:
				spellShapeMode = SpellShapeModeType.Offensive;
			break;
			case SpellShapeType.ball:
				spellShapeMode = SpellShapeModeType.Offensive;
			break;
			case SpellShapeType.wave:
				spellShapeMode = SpellShapeModeType.Defensive;
			break;
			case SpellShapeType.burst:
				spellShapeMode = SpellShapeModeType.Defensive;
			break;
			case SpellShapeType.cloud:
				spellShapeMode = SpellShapeModeType.Defensive;
			break;
		}
	}

}