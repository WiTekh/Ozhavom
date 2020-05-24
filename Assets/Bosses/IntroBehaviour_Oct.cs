using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class IntroBehaviour_Oct : StateMachineBehaviour
{
    private System.Random rnd = new System.Random();
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rand = rnd.Next(0, 3);
        
        if (rand == 0)
        {
            Debug.Log("ROT");
            animator.SetTrigger("pattern_rot");
        }
        else if (rand == 1)
        {
            Debug.Log("SLIDE");
            animator.SetTrigger("pattern_slide");
        }
        else
        {
            Debug.Log("BOING");
            animator.SetTrigger("pattern_boing");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
