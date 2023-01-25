using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using GameManagment;
[RequireComponent(typeof(PlayerManager))]
public class PlayerSpellController : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerSpellInputController playerSpellInputController;
    Vector3 SpellRootPosition => playerManager.RigBonesManager.GetSpellRootPosition();
    float manaChanneled => playerSpellInputController.ManaChanneled;
    

    [SerializeField,ReadOnly]
    private SpellSO currentSpell;
    public SpellSO CurrentSpell => currentSpell;

    private float defaultSpellSize => GameManager.Instance.GameSettings.SpellCastValues.defaultSpellSize;
    private float spellIncreaseSpeed => GameManager.Instance.GameSettings.SpellCastValues.spellIncreaseSpeed;
    private float spellTargetMinDistance => GameManager.Instance.GameSettings.SpellCastValues.spellTargetMinDistance;
    private float spellFireSpeed => GameManager.Instance.GameSettings.SpellCastValues.spellFireSpeed;
    private Vector3 playerPositionOffset => GameManager.Instance.GameSettings.PlayerPositionOffset;

    private float currentSpellSize;
    private GameObject spellInstance;
    private bool spellProcessFire;
    
    PlayerManager targetPlayer => playerManager.opponentPlayer;
    private Vector3 targetPosWithOffset;

    public void Initialize(PlayerManager playerManager) 
    {
        this.playerManager = playerManager;
        playerSpellInputController = playerManager.PlayerSpellInputController;
        playerManager.PlayerAnimationController.onSpellCasted += StartSpellFire;
    }
    void Update()
    {
        if(playerManager.PlayerStateManager.IsInitialized)
        {
            switch(playerManager.PlayerStateManager.CurrentState.ID)
            {
                case "cast":
                {
                    CastSpell();
                }
                break;
                case "fire":
                {
                    if(spellProcessFire)
                    {
                        ProcessSpellFire();
                    }
                }
                break;
            }
        }
    }
    void CastSpell()
    {
        if(playerSpellInputController.TryGetSpell(out SpellSO spell))
        {
            if(currentSpell != spell)
            {
                Destroy(spellInstance);
                spellInstance = InstantiateSpell(spell);
            }

            currentSpell = spell;

            if(spellInstance != null)
            {
                ControlSpellSize();
            }
        }
    }
    void StartSpellFire()
    {
        targetPosWithOffset = targetPlayer.transform.position + playerPositionOffset;
        spellProcessFire = true;
    }
    void ProcessSpellFire()
    {
        float distToTarget = Vector3.Distance(spellInstance.transform.position,targetPosWithOffset);
        if(distToTarget > spellTargetMinDistance)
        {
            spellInstance.transform.position = Vector3.MoveTowards(spellInstance.transform.position,targetPosWithOffset,Time.deltaTime * spellFireSpeed);
        }
        else
        {
            SpellHitTarget();
        }
    }
    private void SpellHitTarget() 
    {
        spellProcessFire = false;
        Destroy(spellInstance);
        //Play spellHit VFX;
        targetPlayer.PlayerStatsController.GetDamage();
        targetPlayer.PlayerAnimationController.Hit();
        ApplySpellBuff(currentSpell);
        FinishRound();
        currentSpell = null;
    }
    void ApplySpellBuff(SpellSO spell)
    {
        //Apply buff and debuff
    }
    void FinishRound()
    {
        playerManager.FinishRoundResolution();
    }
    public void Reset()
    {
        spellProcessFire = false;
        if(spellInstance != null)
            Destroy(spellInstance);
        currentSpellSize = defaultSpellSize;
        currentSpell = null;
    }
    void ControlSpellSize()
    {
        float spellSize = defaultSpellSize + (manaChanneled/100)*spellIncreaseSpeed;
        Vector3 spellSizeVector = new Vector3(spellSize,spellSize,spellSize);
        spellInstance.transform.localScale = spellSizeVector;
    }
    GameObject InstantiateSpell(SpellSO spell)
    {
        GameObject spellGO = Instantiate(spell.SpellShape.ShapePrefab,SpellRootPosition,transform.rotation);
        MeshRenderer spellRenderer = spellGO.GetComponent<MeshRenderer>();
        spellRenderer.material = spell.ElementCombo.ComboMaterial;
        return spellGO;
    }


    private void OnDrawGizmos() {
        if(playerManager?.RigBonesManager?.GetSpellRootPosition() != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(SpellRootPosition,0.3f);
        }
    }
    private void OnDestroy() {
        playerManager.PlayerAnimationController.onSpellCasted -= StartSpellFire;
    }
}
