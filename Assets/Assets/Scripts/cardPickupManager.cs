using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPickupManager : MonoBehaviour
{
    public int cardID; 
    public float speed = 10f; 
    void OnCollisionEnter2D(Collision2D collision){ 
        if(collision.gameObject.CompareTag("Player")){
            Destroy(gameObject); 
        }
    }
    private void Update(){ 
        if(transform.rotation.y >= 180f){
            transform.rotation = new Quaternion(0,0,0,0); 
        }else{
            transform.rotation = new Quaternion(0,transform.rotation.y + (speed * Time.deltaTime),0,transform.rotation.w); 
        }
        
    }
    
}
