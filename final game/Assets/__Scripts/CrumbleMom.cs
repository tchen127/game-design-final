using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleMom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if any child of this object is collided with by the player then...
        // for each child of this object, make it play its respective animation
        // and destroy after 1 second   


        // !!!!! attach crumbleMom to parent of platform objec 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("for each:");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("for each:");
            foreach (Transform child in transform)
            {
                Debug.Log("for each:" + child.gameObject.name);
                child.GetComponent<CrumbleChild>().Crumble();
            }
        }
    }
}
