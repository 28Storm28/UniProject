using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Projectile : MonoBehaviour
{
    // Fireball movement animation comes from https://weisinx7.itch.io/fireballs-sprites 
    public float speed = 10f;          
    public float lifetime = 2f;        

    void Start()
    { 
        Destroy(gameObject, lifetime);
    }
    private void Update(){ 
        transform.position += transform.right * Time.deltaTime * speed; 
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "CardPickup") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            Debug.Log("Card Collided with fireball");
        }
        // Check if the projectile collides with another object (customize the tag as needed)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with an enemy (you can customize this part)
            Debug.Log("Projectile collided with an enemy!");
            Destroy(gameObject);
        }
        // Stop projectiles dying on spawn contact with player
        if (!collision.gameObject.CompareTag("Player")&&!collision.gameObject.CompareTag("CardPickup")&&!collision.gameObject.CompareTag("Fireball")){
            Destroy(gameObject);
        }
        // Destroy the projectile in any case
        
    }

}
