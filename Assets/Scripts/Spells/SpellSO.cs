using UnityEngine;
using NaughtyAttributes;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "_spell", menuName = "Spells/SpellSO", order = 0)]
public class SpellSO : ScriptableObject 
{
	[SerializeField]
	private string spellName;
	public string SpellName => spellName;
	
	[SerializeField,ShowAssetPreview]
	private Sprite spellSprite;
	public Sprite SpellSprite => spellSprite;
	
	[SerializeField]
	private ParticleSystem particlesFX;
	public ParticleSystem ParticlesFX => particlesFX;

	
	[SerializeField,OnValueChanged("ResetValues")]
	private bool isCombo;

	[SerializeField,OnValueChanged("RefreshSpell"),ValidateInput("ValidElement1Type", "Element_1 Can't be none.")]
	private ElementRawType element_1;

	[SerializeField,ShowIf("isCombo"),OnValueChanged("RefreshSpell"),ValidateInput("ValidElement2Type", "Element_2 Can't be none.")]
	private ElementRawType element_2 = ElementRawType.none;



	[SerializeField,ReadOnly]
	private ElementComboType elementComboType;
	public ElementComboType ElementComboType => elementComboType;
	
	[SerializeField,OnValueChanged("RefreshSpell")]
	private SpellShapeType spellShapeType;
	public SpellShapeType SpellShapeType => spellShapeType;

	
	[SerializeField,ReadOnly]
	private ElementComboSO elementCombo;
	public ElementComboSO ElementCombo => elementCombo;
	
	[SerializeField,ReadOnly]
	private SpellShapeSO spellShape;
	public SpellShapeSO SpellShape => spellShape;
	
	
		
	[SerializeField]
	private ElementComboHolderSO elementComboHolderSO;
	
	[SerializeField]
	private SpellShapeHolderSO spellShapeHolderSO;
	
	

	private void ResetValues()
	{   
		element_2 = ElementRawType.none;
		elementComboType = (ElementComboType)((int)element_1);
	}
	private bool ValidElement1Type(ElementRawType elementType)
	{
		return element_1 != ElementRawType.none;
	}
	private bool ValidElement2Type(ElementRawType elementType)
	{
		if(element_1 == element_2 && isCombo)
		{
			isCombo = false;
			element_2 = ElementRawType.none;
			return true;
		}
		else if (element_2 == ElementRawType.none)
		{
			return false;
		}
		return true;
	}
	[Button("Refresh element type")]
	private void RefreshSpell()
	{
		elementComboType = elementComboHolderSO.CombineElements(element_1,element_2);
		elementCombo = elementComboHolderSO.GetElementComboSO(elementComboType);
		spellShape = spellShapeHolderSO.GetSpellShape(spellShapeType);

		if(elementCombo == null)
		{
			Debug.LogError("Combo is null",this);
		}
	}

	public StatsDamage StatsDamage => elementCombo.StatsDamage;
	public StatsResist StatsResist => elementCombo.StatsResist;

	[SerializeField]
	private List<BuffEffect> buffEffects = new List<BuffEffect>();
	public List<BuffEffect> BuffEffects => buffEffects;
}
[Serializable]
public class BuffEffect
{
	[SerializeField]
	private string buffName;
	public string BuffName => buffName;
	
	[SerializeField,ShowAssetPreview]
	private Sprite buffSprite;
	public Sprite BuffSprite => buffSprite;
	
	[SerializeField]
	private ParticleSystem particlesFX;
	public ParticleSystem ParticlesFX => particlesFX;
	
	[SerializeField]
	private BuffCondition buffCondition;
	public BuffCondition BuffCondition => buffCondition;

}
[Serializable]
public class BuffCondition
{
	[SerializeField]
	private BuffTarget target;
	public BuffTarget Target => target;
	
	[SerializeField]
	private BuffOperation operation;
	public BuffOperation Operation => operation;
	
	[SerializeField]
	private BuffStat stat;
	public BuffStat Stat => stat;
	
	[SerializeField]
	private float value;
	public float Value => value;

	[SerializeField]
	[Min(1)]
	private int roundDuration = 1;
	public int RoundDuration => roundDuration;
}
public enum BuffOperation
{
	gain = 1,
	lose = 2,
	gainPerRound = 3,
	losePerRound = 4,
}
public enum BuffStat
{
	pierce = 1,
	force = 2,
	aerosol = 3,
	deflection = 4,
	toughness = 5,
	push = 6,
	current_mana = 7,
	current_hp = 8,
	manaReplenishRate = 9,
}
public enum BuffTarget
{
	self = 1,
	enemy = 2,
	both = 3,
}