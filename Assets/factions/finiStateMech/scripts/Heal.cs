using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : NPCBase
{
     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<TankAI>().StartHeal();
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
}
