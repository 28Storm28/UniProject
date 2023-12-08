using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class topHatPatrol : StateMachineBehaviour
{
    public Vector2 pointA; 
    public Vector2 pointB; 
    public GameObject self; 
    public Vector2 currentPoint; 
    private Rigidbody2D rb; 
    public hatPatrolVariables vars; 
    public GameObject marker; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>(); 
        self = animator.gameObject; 
        vars = animator.GetComponent<hatPatrolVariables>(); 
        pointA = self.transform.position; 
        pointB = self.transform.position; 
        pointA = new Vector2(self.transform.position.x + vars.aOffset, self.transform.position.y);
        Debug.Log(pointA); 
        pointB = new Vector2(self.transform.position.x + vars.bOffset, self.transform.position.y);
        Debug.Log(self.transform.position); 
        Debug.Log(pointB);   
        currentPoint = pointA; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self.transform.position = Vector2.MoveTowards(rb.transform.position, currentPoint, vars.patrolSpeed * Time.deltaTime); 
        if(Vector2.Distance(self.transform.position, currentPoint) < 0.5f){
            if(currentPoint == pointA){
                currentPoint = pointB; 
            }else{
                currentPoint = pointA; 
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
