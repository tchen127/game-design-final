using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform_Crumble : MonoBehaviour
{

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collision");
        if (coll.gameObject.tag == "Player")
        {
            Destroy(this);
        }
    }
   

}
