using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    public Transform pivot;

    public float speed;
    //public float rotSpeed = 1.0f;
    public float accuracy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        speed = NPC.GetComponent<Enime>().speed;

        //opponent = NPC.GetComponent<TankAI>().GetPlayer();
        opponent = NPC.GetComponent<TankAI>().GetClosest();
        pivot = NPC.GetComponent<TankAI>().pivot;

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            opponent = NPC.GetComponent<TankAI>().GetClosest();

    }

}