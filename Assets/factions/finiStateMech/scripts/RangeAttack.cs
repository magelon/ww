using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<TankAI>().StartRange();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //NPC.transform.LookAt(opponent.transform.position);
        if (opponent)
        {
            if (pivot.position.x < opponent.GetComponent<TankAI>().pivot.position.x)
            {
                if (NPC.GetComponent<TankAI>().face == false)
                {
                    NPC.GetComponent<TankAI>().Flip();
                }
            }
            if (pivot.position.x > opponent.GetComponent<TankAI>().pivot.position.x)
            {
                if (NPC.GetComponent<TankAI>().face == true)
                {
                    NPC.GetComponent<TankAI>().Flip();
                }
            }
        }

        NPC.GetComponent<TankAI>().GetClosest();
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
