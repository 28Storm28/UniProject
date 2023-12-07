using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="new Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description; 
    public int attack; 
    public int health; 
    public Sprite artwork; 

}
