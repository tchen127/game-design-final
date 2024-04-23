using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Keeps track of the number of followers the player currently has, during the game. 
/// ("Followers" meaning the dinosaurs that the player rescues that they have come into contact with)
/// </summary>
public class FollowerCount : MonoBehaviour
{
    public int followerCount; // The current number of followers the player has
    public TMP_Text text; // The TextMeshPro object to display

    [Header("Debugging")]
    [SerializeField] private bool debugOn;

    /// <summary>
    /// Initialize follower count to 0 at the beginning of the game
    /// </summary>
    void Start(){
        followerCount = 0;
    }

    /// <summary>
    /// Every frame, update the UI text to show the correct follower count number
    /// </summary>
    void Update()
    {
        text.SetText("Dinos: " + followerCount + "/3");
        if (debugOn) Debug.Log("Set FollowerCount text");
    }

    /// <summary>
    /// Helper method used by the Follower script used to increment followerCount by 2
    /// </summary>
    public void IncrementFollowerCount()
    {
        followerCount++;
        if (debugOn) Debug.Log("Incremented FollowerCount");
    }

    /// <summary>
    /// Helper method used by the Follower script used to decrement followerCount by 2
    /// </summary>
    public void DecrementFollowerCount()
    {
        followerCount--;
        if (debugOn) Debug.Log("Decremented FollowerCount");
    }
}
