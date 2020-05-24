using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boingo_Behavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("boing_to_idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
