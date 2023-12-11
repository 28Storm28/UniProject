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
