using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //static point of interest
    static public GameObject POI; 

    void FixedUpdate(){
        Vector3 pos = Vector3.zero;

        //calculate position that camera needs to be at
        pos = POI.transform.position;
        pos.x = 0; 
        pos.z = transform.position.z;

        //move camera to pos
        transform.position = pos;
    }
}
