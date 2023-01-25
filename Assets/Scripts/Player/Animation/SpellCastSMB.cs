using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastSMB : StateMachineBehaviour
{
    [SerializeField] float delayToFireSpell;
    [SerializeField]
    PlayerManager playerManager;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        playerManager = animator.GetComponentInParent<PlayerManager>();
        Timer.StartSimpleFloatTimer("delaySpellFire_"+playerManager.PlayerNum,delayToFireSpell,delegate{playerManager.PlayerAnimationController.onSpellCasted?.Invoke();});
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.ResetTrigger("FireSpell");
    }
}
