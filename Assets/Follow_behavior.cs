using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_behavior : StateMachineBehaviour
{
    private Transform playerpos;
    public float range;
    public float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(playerpos + "Joueur");
        Debug.Log(animator.transform.position + "Ennemi");
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerpos.position, speed);
        //if (Vector2.Distance(animator.transform.position, playerpos.position) <= range)
      //  {
       //     Debug.Log("IsClose");
       //     animator.SetBool("isClose",true);
      //  }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
