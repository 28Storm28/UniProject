using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Card_display : MonoBehaviour
{
    public Card card; 
    public Text nameText; 
    public Text Description; 
    public Image artWork; 
    public Text Attack; 
    public Text Health;
    public Color color; 

    // Start is called before the first frame update
    void Start()
    {   
        update_card_display(); 
    }
    void update_card_display(){
        nameText.text = card.name; 
        Description.text = card.description; 
        artWork.sprite = card.artwork; 
        Attack.text = card.attack.ToString(); 
        Health.text = card.health.ToString();
    }

}
