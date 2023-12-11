using System.Collections;
using System.Collections.Generic;
using System.Text;
using NavMeshPlus.Extensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class birdManager : MonoBehaviour
{
    [SerializeField] public Transform target; 
    NavMeshAgent agent; 
    private Rigidbody2D rb;
    public bool Attacking; 
    private Animator am; 
    public bool Dying; 
    public bool Dead; 
    private Vector2 startPos; 
    public bool goingHome; 
    private GameObject homeRadiusChild; 
    private GameObject homeRadius;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>(); 
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        Attacking = false;  
        goingHome = false; 
        Dead = false; 
        Dying = false; 
        startPos = transform.position; 
        rb = gameObject.GetComponent<Rigidbody2D>(); 
        am = gameObject.GetComponent<Animator>(); 
        homeRadiusChild = gameObject.transform.GetChild(0).gameObject; 
        homeRadius = Instantiate(homeRadiusChild, homeRadiusChild.transform.position, homeRadiusChild.transform.rotation); 
        //Adjust to account for parent child scaling so that the new object won't be scaled weirdly
        homeRadius.transform.localScale = Vector3.Scale(gameObject.transform.localScale, homeRadiusChild.transform.localScale);
        Destroy(homeRadiusChild); 
    }
    void Death(){ 
        //Stop the dead entity from killing the player on collision 
        gameObject.tag = "Untagged"; 
        am.SetTrigger(name:"dying"); 
        agent.enabled = false; 
        Attacking = false; 
        Dead = true; 
    }
    void RTB(){ 
        agent.SetDestination(startPos);
        if(Vector2.Distance(gameObject.transform.position,startPos) < 0.5f){
            goingHome = false; 
        }
    }
    void FixedUpdate()
    {

        if(!Dead){
            if(homeRadius.GetComponent<batHomeRadius>().playerWithin){
                Attacking = true; 
            }
            if(Dying){
                Death(); 
            }
            if(Attacking){
                if(!homeRadius.GetComponent<batHomeRadius>().playerWithin){
                    goingHome = true; 
                }
                agent.SetDestination(target.position);
            }   
            if(goingHome){
                RTB();
            }
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision2D) {
        if(collision2D.collider.CompareTag("Player")){
            rb.velocity = new Vector2(0,0);
            goingHome = true;  
        }
        if(collision2D.collider.CompareTag("Fireball")){
            rb.velocity = new Vector2(0,0);
            Dying = true; 
        }
    }
}
