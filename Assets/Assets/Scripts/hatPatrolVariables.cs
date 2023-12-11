using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class hatPatrolVariables : MonoBehaviour
{
    public float aOffset = 1; 
    public float bOffset = 1;
    public float patrolSpeed = 0.025f; 
    public Vector2 pointA; 
    public Vector2 pointB; 
    private Animator an; 
    void Start(){
        pointA = new Vector2(transform.position.x + aOffset, transform.position.y); 
        pointB = new Vector2(transform.position.x + bOffset, transform.position.y); 
        an = gameObject.GetComponent<Animator>(); 
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Fireball")){
            an.SetTrigger("Dead");
            gameObject.tag = "Untagged"; 
        }
    }
}
