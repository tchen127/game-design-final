using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent platform object contains left, middle, and right blocks. 
/// Activate each platform's animation individually. 
/// </summary>
public class CrumbleMom : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D platform)
    {
        // Check if player touches the platform
        if (platform.gameObject.CompareTag("Player"))
        {
            // for each block generated 
            foreach (Transform child in transform)
            {
                // Get CrumbleChild script to enable animation
                child.GetComponent<CrumbleChild>().Crumble();
            }

            // Destroy the parent platform after 1 second
            Destroy(gameObject, 1);
        }
    }

}
