using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SpellsHolder", menuName = "Spells/SpellsHolderSO", order = 0)]
public class SpellsHolderSO : ScriptableObject {

	[SerializeField]
	private List<SpellSO> allSpells = new List<SpellSO>();
	public List<SpellSO> AllSpells => allSpells;

	public SpellSO GetSpell(ElementComboType elementComboType, SpellShapeType spellShapeType)
	{
		return allSpells.Find(s=>s.SpellShapeType == spellShapeType && s.ElementComboType == elementComboType);
	}

}