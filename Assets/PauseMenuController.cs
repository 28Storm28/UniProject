using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class PauseMenuController : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(menuCanvas.activeSelf){
                menuCanvas.SetActive(false);  
                Time.timeScale = 1; 
            }else{
                menuCanvas.SetActive(true); 
                Time.timeScale = 0.1f;  
            }
        }
    }
    public void restartLevel(){ 
        Debug.Log("Restarting"); 
        SceneManager.LoadScene(currentScene.name); 
    }
}
