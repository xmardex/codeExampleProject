using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SpellShapesHolder", menuName = "Spells/SpellShapeHolderSO", order = 0)]
public class SpellShapeHolderSO : ScriptableObject 
{
	[SerializeField]
	private List<SpellShapeSO> allSpellShapes = new List<SpellShapeSO>();
	public List<SpellShapeSO> AllSpellShapes => allSpellShapes;

	public SpellShapeSO GetSpellShape(SpellShapeType spellShapeType)
	{
		return allSpellShapes.Find(ss=>ss.SpellShapeType == spellShapeType);
	}
}