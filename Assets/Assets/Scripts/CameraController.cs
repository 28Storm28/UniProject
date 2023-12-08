using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public float FollowSpeed; 
    public float xOffset; 
    public float yOffset; 
    public float inputOffsetX; 
    public float inputOffsetY; 
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x+xOffset,target.position.y+yOffset,-10f);
        transform.position= Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);  
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            xOffset += inputOffsetX;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            xOffset -= inputOffsetX;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            yOffset += inputOffsetY;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            yOffset -= inputOffsetY;
        }
        if(Input.GetKeyUp(KeyCode.RightArrow)){
            xOffset -= inputOffsetX;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)){
            xOffset += inputOffsetX;
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            yOffset -= inputOffsetY;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow)){
            yOffset += inputOffsetY;
        }
    }
}
