using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBase
{
   
    public waypoints[] wayP;
    //public Vector2[] sinWP;
    //which way towards
    int currentWP;

    //get at runtime
    private void Awake()
    {
        //add tag waypoint to waypoint object
        //wayP = GameObject.FindGameObjectsWithTag("WayPoint");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //start patrol from 0 position
        currentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.speed = NPC.GetComponent<Enime>().speed;

        NPC.GetComponent<TankAI>().GetClosest();
        //Debug.Log(Vector3.Distance(wayP[currentWP].transform.position,
        //NPC.transform.position));
        if (wayP.Length == 0) return;
        if(Vector2.Distance(wayP[currentWP].wp,new Vector2(pivot.position.x, pivot.position.y)
        ) < accuracy)
        {
            currentWP++;
            if (currentWP >= wayP.Length)
            {
                currentWP = 0;
            }
        }
        
        if (pivot.position.x <= wayP[currentWP].wp.x) {
            if (NPC.GetComponent<TankAI>().face == false)
            {
                NPC.GetComponent<TankAI>().Flip();
            }
            NPC.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (pivot.position.x >= wayP[currentWP].wp.x)
        {
            if (NPC.GetComponent<TankAI>().face == true)
            {
                NPC.GetComponent<TankAI>().Flip();
            }
            NPC.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
