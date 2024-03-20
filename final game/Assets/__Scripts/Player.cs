using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Inscribed")]
    public float speed = 20;

    void Awake()
    {
        FollowCam.POI = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //check axis for horizontal movement
        float xAxis = Input.GetAxis("Horizontal");

        //change player position based on input
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        transform.position = pos;
    }
}
