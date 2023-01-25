using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ElementComboHolderSO", menuName = "Spells/ElementComboHolder", order = 0)]
public class ElementComboHolderSO : ScriptableObject 
{
	[SerializeField]
	private List<ElementComboSO> allElementCombos = new List<ElementComboSO>();
	public List<ElementComboSO> AllElementCombos => allElementCombos;

	public ElementComboSO GetElementComboSO(ElementComboType elementComboType)
	{
		return allElementCombos.Find(e=>e.ElementComboType == elementComboType);
	}

	public ElementComboType CombineElements(ElementRawType element_1, ElementRawType element_2)
	{	
		ElementComboType spellComboType = ElementComboType.fire;

		if(element_1 == ElementRawType.none && element_2 == ElementRawType.none)
		{
			Debug.LogError("Elements can't be null: " + element_1+"/"+element_2);
		}

		if(element_2 == element_1 || element_2 == ElementRawType.none)
		{
			spellComboType = (ElementComboType)((int)element_1);
		}
		switch (element_2)
		{
			case ElementRawType.fire:
			{
				switch (element_1)
				{
					case ElementRawType.earth:
						spellComboType = ElementComboType.lava;
					break;
					case ElementRawType.air:
						spellComboType = ElementComboType.plasma;
					break;
					case ElementRawType.water:
						spellComboType = ElementComboType.steam;
					break;
				}
			}
			break;
			case ElementRawType.air:
			{
				switch (element_1)
				{
					case ElementRawType.fire:
						spellComboType = ElementComboType.plasma;
					break;
					case ElementRawType.earth:
						spellComboType = ElementComboType.lightning;
					break;
					case ElementRawType.water:
						spellComboType = ElementComboType.ice;
					break;
				}
			}
			break;
			case ElementRawType.water:
			{
				switch (element_1)
				{
					case ElementRawType.fire:
						spellComboType = ElementComboType.steam;
					break;
					case ElementRawType.earth:
						spellComboType = ElementComboType.mud;
					break;
					case ElementRawType.air:
						spellComboType = ElementComboType.ice;
					break;
				}
			}
			break;
			case ElementRawType.earth:
			{
				switch (element_1)
				{
					case ElementRawType.fire:
						spellComboType = ElementComboType.lava;
					break;
					case ElementRawType.air:
						spellComboType = ElementComboType.lightning;
					break;
					case ElementRawType.water:
						spellComboType = ElementComboType.mud;
					break;
				}
			}
			break;
		}
		return spellComboType;
	}
}