using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using System;
using GameManagment;
using Random = UnityEngine.Random;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
public class PlayerSpellInputController : MonoBehaviour
{
    private PlayerManager playerManager;
    public PlayerManager PlayerManager => playerManager;
    
    [SerializeField]
    private ElementComboHolderSO elementComboHolderSO;
    [SerializeField]
    private SpellShapeHolderSO spellShapeHolderSO;
    [SerializeField]
    private SpellsHolderSO spellsHolderSO;  
    [SerializeField]
    private string controlSchemeName;
    [SerializeField]
    public InputTiming inputTiming;

    [SerializeField]
    private SpellInputData currentSpellData;
    public SpellInputData CurrentSpellData => currentSpellData;

    [SerializeField]
    public float manaChanneled;
    public float ManaChanneled => manaChanneled;

    [SerializeField]
    private CastedSpellData castedSpellData;
    public CastedSpellData CastedSpellData => castedSpellData;
    private SpellSO currentSpell;

    private PlayerInput playerInput;
    
    public bool isInputAllowed;

    public void Initialize(PlayerManager playerManager) 
    {
        this.playerManager = playerManager;
        currentSpellData = new SpellInputData(elementComboHolderSO,spellShapeHolderSO,spellsHolderSO, this);
        playerInput = GetComponent<PlayerInput>();
        GlobalEventsManager.Instance.roundEvents.onRoundCasting += RoundCast;
        GlobalEventsManager.Instance.roundEvents.onRoundResolution += RoundResolution;

        if(!playerManager.IsAIControlled)
        {
            playerInput.SwitchCurrentControlScheme(controlSchemeName,Keyboard.current);
            playerInput.onActionTriggered += InputActionProcess;
        }
    }
    void InputActionProcess(CallbackContext context)
    {
        if(isInputAllowed)
        {
            InputActionPhase actionPhase = context.action.phase;

            if(actionPhase == InputActionPhase.Started)
            {
                ProcessPress(context);
            }
            if(actionPhase == InputActionPhase.Canceled)
            {
                ProcessRelease(context);
            }
        }
    }
    void ProcessPress(CallbackContext context)
    {
        ProcessElementPressInput(context);
        ProcessShapePressInput(context);
    }
    void ProcessRelease(CallbackContext context)
    {
        ProcessElementReleaseInput(context);
    }
    void ProcessElementPressInput(InputAction.CallbackContext context)
    {

        ElementRawType pressedElement = context.action.name switch
        {
            "Fire" => ElementRawType.fire,
            "Earth" => ElementRawType.earth,
            "Water" => ElementRawType.water,
            "Air" => ElementRawType.air,
            _ => ElementRawType.none,
        };
        
        if(pressedElement != ElementRawType.none)
            ElementPressed(pressedElement);
    }
    void ProcessElementReleaseInput(InputAction.CallbackContext context)
    {
        ElementRawType releasedElement = context.action.name switch
        {
            "Air" => ElementRawType.air,
            "Water" => ElementRawType.water,
            "Earth" => ElementRawType.earth,
            "Fire" => ElementRawType.fire,
            _ => ElementRawType.none,
        };

        if(releasedElement != ElementRawType.none)
            ElementReleased(releasedElement);

    }
    void ProcessShapePressInput(CallbackContext context)
    {
        switch(context.action.name)
        {   
            case "Bolt/Wave_ShapeOrOffensiveMode":
            {
                if(currentSpellData.CanSelectShape)
                    currentSpellData.SetShape(SpellShapeType.bolt,SpellShapeType.wave);
                else
                    currentSpellData.SelectSpellShapeMode(SpellShapeModeType.Offensive);
            }
            break;
            case "Barrage/Burst_ShapeOrDefensiveMode":
            {
                if(currentSpellData.CanSelectShape)
                    currentSpellData.SetShape(SpellShapeType.barrage,SpellShapeType.burst);
                else
                    currentSpellData.SelectSpellShapeMode(SpellShapeModeType.Defensive);
            }
            break;
            case "Ball/Cloud_Shape":
            {
                currentSpellData.SetShape(SpellShapeType.ball,SpellShapeType.cloud);
            }
            break;
        }
    }

    public bool TryGetSpell(out SpellSO spell)
    {
        if(currentSpellData.TryGetSpell(out spell))
        {   
            currentSpell = spell;
            return true;
        }
        else
            return false;
    }
    void RoundCast()
    {
        if(!playerManager.IsAIControlled)
        {
            isInputAllowed = true;
        }
        else
        {
            //RandomSpell
            Timer.StartSimpleFloatTimer("AICastDelay",GameManager.Instance.GameSettings.InputTiming.timeToStartManaChanneling,delegate
            {
                AIInput();
            });
        }
    }
    void AIInput()
    {
        currentSpellData.RandomSpell();

        float randomManaChannellingTime = Random.Range(
            GameManager.Instance.GameSettings.AISpellCast.minManaChannellingTime,
            GameManager.Instance.GameSettings.AISpellCast.maxManaChannellingTime
        );

        playerManager.PlayerStatsController.AIStartManaChannelling(randomManaChannellingTime,currentSpellData);
    }
    void RoundResolution()
    {
        isInputAllowed = false;
        castedSpellData = new CastedSpellData(currentSpell,manaChanneled);
        playerManager.PlayerStatsController.StopManaChannelling();
    }
    public void Reset()
    {
        //reset all for new round
        currentSpellData = new SpellInputData(elementComboHolderSO,spellShapeHolderSO,spellsHolderSO, this);
    }
    void ElementPressed(ElementRawType elementRaw)
    {
        currentSpellData.PressElement(elementRaw);
        playerManager.PlayerStatsController.StartManaChannelling(currentSpellData);
    }
    void ElementReleased(ElementRawType elementRaw)
    {
        currentSpellData.ReleaseElement(elementRaw);
        if(!currentSpellData.Element_1.isHolded && !currentSpellData.Element_2.isHolded)
            playerManager.PlayerStatsController.StopManaChannelling();
    }
    private void OnDestroy() 
    {
        if(!playerManager.IsAIControlled)
        playerInput.onActionTriggered -= InputActionProcess;

        GlobalEventsManager.Instance.roundEvents.onRoundCasting -= RoundCast;
        GlobalEventsManager.Instance.roundEvents.onRoundResolution -= RoundResolution;
    }
}
[Serializable]
public class CastedSpellData
{
    public SpellSO spell;
    public float manaChanneled;

    public CastedSpellData(SpellSO spell, float manaChanneled)
    {
        this.spell = spell;
        this.manaChanneled = manaChanneled;
    }
}
