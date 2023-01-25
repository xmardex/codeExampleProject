using UnityEngine;

public class IdleRandomSMB : StateMachineBehaviour
{
    [SerializeField]
    private string rndParameterName = "rnd_IdleID";
    [SerializeField]
    private int idleClipsCount = 2;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        int randomID = Random.Range(0,idleClipsCount);
        animator.SetInteger(rndParameterName,randomID);
    }
}
