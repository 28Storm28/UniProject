using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class gameOverController : MonoBehaviour
{
    public Scene currentScene; 
    public GameObject menuCanvas; 
    private void Start() {
        Time.timeScale = 1; 
        currentScene = SceneManager.GetActiveScene(); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void restartLevel(){ 
        Debug.Log("Restarting" + currentScene.name); 

        SceneManager.LoadScene(currentScene.name); 
    }
    public void exitToMenu(){ 
        SceneManager.LoadScene("MainMenu");
    }
}
