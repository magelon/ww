﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<TankAI>().StopFiring();
        NPC.GetComponent<TankAI>().StopRange();
        NPC.GetComponent<TankAI>().StopHeal();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rotate towards target

        //var direction = opponent.transform.position - NPC.transform.position;
        //NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
        //Quaternion.LookRotation(direction),
        //rotSpeed * Time.deltaTime);
        if (opponent)
        {
            if (pivot.position.x < opponent.GetComponent<TankAI>().pivot.position.x)
            {
                if (NPC.GetComponent<TankAI>().face == false)
                {
                    NPC.GetComponent<TankAI>().Flip();
                }
                NPC.transform.Translate(Vector2.right * Time.deltaTime * speed);
            }

            if (pivot.position.x > opponent.GetComponent<TankAI>().pivot.position.x)
            {
                if (NPC.GetComponent<TankAI>().face == true)
                {
                    NPC.GetComponent<TankAI>().Flip();
                }
                NPC.transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        }
   
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
