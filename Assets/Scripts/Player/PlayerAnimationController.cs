using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : BaseScript
{
    private PlayerManager playerManager;
    private Animator animator;
    [HideInInspector]
    public Animator Animator {set => animator = value;}
    [SerializeField]
    private RuntimeAnimatorController baseAnimatorController; 
    public Action onSpellCasted;

    public void Initialize(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        animator.runtimeAnimatorController = baseAnimatorController;
        animator.SetFloat("currentHP",playerManager.PlayerStatsController.CurrentStats.currentHealth);
    }
    public void Reset()
    {
        Idle();
    }
    public void Greeting()
    {
        animator.SetTrigger("Greeting");
    }
    public void CastSelectedSpell()
    {
        SpellSO spell = playerManager.PlayerSpellCaster.CurrentSpell;
        if(spell != null)
        {   
            animator.SetInteger("spellShapeID",(int)spell.SpellShapeType);
            animator.SetTrigger("FireSpell");
        }
    }
    public void Idle()
    {
        animator.SetTrigger("Idle");
    }
    public void Lose()
    {
        animator.SetBool("GameOver",true);
        animator.SetTrigger("Lose");
    }
    public void Win()
    {
        animator.SetBool("GameOver",true);
        animator.SetTrigger("Win");
    }
    public void Hit()
    {
        animator.SetFloat("currentHP",playerManager.PlayerStatsController.CurrentStats.currentHealth);
        animator.SetTrigger("Hit");
    }
}
