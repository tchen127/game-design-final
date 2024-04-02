using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{
    [Header("Inscribed")]
    static public GameObject POI; 
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Vector3.zero;

        //calculate position that camera needs to be at
        pos = POI.transform.position;
        pos.x = 0; 
        pos.z = transform.position.z;

        //move camera to pos
        this.transform.position = pos; 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 0){
        //move camera down at a constant speed
        Vector3 camPos = transform.position;
        camPos.y -= speed * Time.deltaTime;
        transform.position = camPos;
        }
        
    }
}
