using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class endMenuController : MonoBehaviour
{
    public void End(){ 
        Application.Quit(); 
    }
    public void MainMenu() {
         SceneManager.LoadScene("MainMenu");
    }
}
