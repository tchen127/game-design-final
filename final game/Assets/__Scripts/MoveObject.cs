using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        //move object up at a constant speed
        Vector3 objPos = transform.position;
        objPos.y += speed * Time.deltaTime;
        transform.position = objPos;
        
    }
}
