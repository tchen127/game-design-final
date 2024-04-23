using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays appropriate animation for each of crumble platform block (left, middle, and right) 
/// based on the tag of the game object. 
/// </summary>

public class CrumbleChild : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Get Animator for each platform block object 
        animator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Animator plays the corresponding animation for each crumble block based on its tag. 
    /// </summary>
    public void Crumble()
    {
        if (gameObject.CompareTag("Crumble_left"))
        {
            animator.Play("Crumble_L");
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
            Debug.LogError("No tag on crumble block.");
        }
    }
}
