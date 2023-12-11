using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class gameStateController : MonoBehaviour
{
    public GameObject player; 
    public int health = 3; 
    public UnityEngine.UI.Image health1,health2,health3; 
    public Sprite fullHeart; 
    public Sprite emptyHeart;  
    public TMP_Text ammo; 
    public GameObject gameOver;  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ammo.SetText("Ammo: " + player.GetComponent<movement_controller>().ammo); 
        health = player.GetComponent<movement_controller>().health; 
        if(health < 3){
            health3.sprite = emptyHeart; 
        }
        if(health < 2){
            health2.sprite= emptyHeart; 
        }
        if(health < 1){
            health1.sprite= emptyHeart; 
            gameOver.SetActive(true); 
            Destroy(gameObject); 
        }
        
    }

}
