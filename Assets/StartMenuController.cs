using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class StartMenuController : MonoBehaviour {
    public Scene tutorial; 
    public void startGame(){ 
        SceneManager.LoadScene("Level1");
    }
    public void startTutorial(){
        SceneManager.LoadScene("Tutorial");  
    }
    public void Quit(){ 
        Debug.Log("Program is quitting"); 
        Application.Quit(); 
    }
}