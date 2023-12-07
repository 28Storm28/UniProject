using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public float FollowSpeed; 
    public float xOffset; 
    public float yOffset; 

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x+xOffset,target.position.y+yOffset,-10f);
        transform.position= Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);  
    }
}
