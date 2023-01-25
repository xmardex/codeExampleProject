using UnityEngine;

[CreateAssetMenu(fileName = "_elementCombo", menuName = "Spells/ElementComboSO", order = 0)]
public class ElementComboSO : ScriptableObject 
{
	[SerializeField]
	private ElementComboType elementComboType;
	public ElementComboType ElementComboType => elementComboType;
	[SerializeField]
	private Material comboMaterial;
	public Material ComboMaterial => comboMaterial;
	
	[SerializeField]
	private StatsDamage statsDamage;
	public StatsDamage StatsDamage => statsDamage;
	
	[SerializeField]
	private StatsResist statsResist;
	public StatsResist StatsResist => statsResist;

}