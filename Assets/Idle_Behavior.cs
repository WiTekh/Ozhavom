using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class Idle_Behavior : StateMachineBehaviour
{
    private System.Random rnd = new System.Random();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rand = rnd.Next(1,4);
        if (rand == 2)
        {
            Debug.Log("ROT");
            animator.SetTrigger("pattern_rot");
            rand++;
        }
        else if (rand == 3)
        {
            Debug.Log("SLIDE");
            animator.SetTrigger("pattern_slide");
            rand++;
        }
        else 
        {
            Debug.Log("BOING");
            animator.SetTrigger("pattern_boing");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
