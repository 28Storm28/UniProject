using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCollider : MonoBehaviour
{
    public Camera cam; 
    public Transform target; 
    public Transform prevTarget;
    public float size; 
    private float prevSize; 




    /*


    I cannot find the video I adapted this camera controller from as it was a 15 second youtube short that doesn't 
    seem to be up anymore. Be aware that this code is adapted from another source and may show similarities to other code


    */
    void Start()
    {
       cam = Camera.main; 
       prevTarget = cam.GetComponent<CameraController>().target;
       prevSize = cam.orthographicSize; 
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Collider2D>().CompareTag("Player")){
            Debug.Log("Player Enter");
            cam.GetComponent<CameraController>().setTarget(target); 
            cam.orthographicSize = size;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.GetComponent<Collider2D>().CompareTag("Player")){
            Debug.Log("Player Leave");
            cam.GetComponent<CameraController>().setTarget(prevTarget); 
            cam.orthographicSize = prevSize; 
        }
        
    }

}
