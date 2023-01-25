using System;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using GameManagment;
[Serializable]
public class SpellInputData
{
    //Holders
    
    ElementComboHolderSO elementComboHolderSO;
    SpellShapeHolderSO spellShapeHolderSO;
    SpellsHolderSO spellsHolderSO;

    //SpellData
    [ReadOnly][SerializeField]
    private ElementButton element_1;
    public ElementButton Element_1 => element_1;
    [ReadOnly][SerializeField]
    private ElementButton element_2;
    public ElementButton Element_2 => element_2;
    [HideInInspector]
    public SpellShapeType spellOppositeShapeType;
    [ReadOnly]
    public SpellShapeModeType spellShapeModeType;
    [ReadOnly]
    public ElementComboType elementComboType; 
    [ReadOnly]
    public SpellShapeType spellShapeType;

    //Checks
    [HideInInspector]
    public bool isModeChosen;
    [HideInInspector]
    public bool isShapeChosen;

    public bool CanSelectShape => isModeChosen && !isShapeChosen;

    private PlayerSpellInputController playerSpellInputController;

    //SpellOutput
    public bool TryGetSpell(out SpellSO spell)
    {   
        spell = spellsHolderSO.GetSpell(elementComboType,spellShapeType);
        return spell != null;
    }
    
    public SpellInputData(ElementComboHolderSO elementComboHolderSO, SpellShapeHolderSO spellShapeHolder, SpellsHolderSO spellsHolderSO, PlayerSpellInputController playerSpellInputController)
    {
        this.elementComboHolderSO = elementComboHolderSO;
        this.spellsHolderSO = spellsHolderSO;
        this.spellShapeHolderSO = spellShapeHolder;
        this.playerSpellInputController = playerSpellInputController;

        spellShapeType = SpellShapeType.bolt;
        element_1 = new ElementButton(false,ElementRawType.fire);
        element_2 = new ElementButton(false,ElementRawType.fire);

        isModeChosen = false;
        isShapeChosen = true;
        //Select default
        CombineElements();
        SelectSpellShapeMode(SpellShapeModeType.Offensive);
        SetShape(SpellShapeType.bolt,SpellShapeType.burst);
        
    }
    public void PressElement(ElementRawType element)
    {

        if(!element_1.isHolded)
        {
            element_1.isHolded = true;
            element_1.elementRawType = element;

            if(element_2.elementRawType == ElementRawType.none || !element_2.isHolded)
                element_2.elementRawType = element;

            CombineElements();
            return;
        }
        if(!element_2.isHolded && element_1.elementRawType != ElementRawType.none)
        {
            element_2.isHolded = true;
            element_2.elementRawType = element;

            CombineElements();
            return;
        }

        if(element_1.isHolded && element_2.isHolded)
        {
            element_2.elementRawType = element;
            
            CombineElements();
            return;
        }
    }
    public void RandomSpell()
    {
        int rnd_Shape = Random.Range(0,6);
        SpellShapeType randomShape = (SpellShapeType)rnd_Shape;
        spellShapeType = randomShape;

        int rnd_Element1 = Random.Range(1,5);
        int rnd_Element2 = Random.Range(1,5);

        ElementRawType randomElement1 = (ElementRawType)rnd_Element1;
        ElementRawType randomElement2 = (ElementRawType)rnd_Element2;

        element_1.elementRawType = randomElement1;
        element_2.elementRawType = randomElement2;

        CombineElements();
    
    }
    void CombineElements()
    {
        elementComboType = elementComboHolderSO.CombineElements(element_1.elementRawType,element_2.elementRawType);
    }
    public void ReleaseElement(ElementRawType elementRaw)
    {
        if(element_1.elementRawType == elementRaw)
            element_1.isHolded = false;


        if(element_2.elementRawType == elementRaw)
            element_2.isHolded = false;
    }
    public void SelectSpellShapeMode(SpellShapeModeType spellShapeModeType)
    {
        float timeToResetSelection = GameManager.Instance.GameSettings.InputTiming.timeToResetShapeSelectionIfThereIsNothingSelected;
        this.spellShapeModeType = spellShapeModeType;

        isModeChosen = true;
        isShapeChosen = false;

        Timer.StartSimpleFloatTimer("selectFormTimer",timeToResetSelection,delegate
        {
            isModeChosen = false;
            isShapeChosen = true;
        });
    }
    public void SetShape(SpellShapeType offensiveShape, SpellShapeType defensiveShape)
    {
        spellShapeType = 
            spellShapeModeType == SpellShapeModeType.Offensive ?
                offensiveShape : defensiveShape;
        spellOppositeShapeType = spellShapeModeType == SpellShapeModeType.Offensive ? defensiveShape : offensiveShape;
            
        isModeChosen = false;
        isShapeChosen = true;
        
        Timer.StopSimpleTimerFloatInstance("selectFormTimer");
    }

}
[Serializable]
public class ElementButton
{
    public bool isHolded;
    public ElementRawType elementRawType;

    public ElementButton(bool isHolded, ElementRawType elementRawType)
    {
        this.isHolded = isHolded;
        this.elementRawType = elementRawType;
    }
}