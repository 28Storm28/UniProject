using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batHomeRadius : MonoBehaviour
{
    public bool playerWithin; 
    // Start is called before the first frame update
    void Start()
    {
        playerWithin = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Collider2D>().CompareTag("Player")){
            playerWithin = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.GetComponent<Collider2D>().CompareTag("Player")){
            playerWithin = false; 
        }
    }
}
