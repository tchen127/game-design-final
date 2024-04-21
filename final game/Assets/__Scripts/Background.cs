using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    //position of bottom middle of camera
    private Vector2 cameraBottom;

    //called on first frame
    void Start()
    {
        //global position of bottom of camera
        cameraBottom =  Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        //destroy the MoveObject script when the bottom of the background reaches the bottom of the camera view
        if (transform.position.y >= cameraBottom.y) Destroy(this.GetComponent<MoveObject>());
    }
}
