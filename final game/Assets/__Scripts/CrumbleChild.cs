using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleChild : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void Crumble()
    {
        // Debug.Log("play animation");

        if (gameObject.CompareTag("Crumble_left"))
        {
            animator.Play("Crumble_L");
            Debug.Log("crumble left animation played");
        }
        else if (gameObject.CompareTag("Crumble_middle"))
        {
            animator.Play("Crumble_M");
        }
        else if (gameObject.CompareTag("Crumble_right"))
        {
            animator.Play("Crumble_R");
        }
        else
        {
            Debug.Log("no crumble");
        }

        Destroy(gameObject, 1);

    }
}
