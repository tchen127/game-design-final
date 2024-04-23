using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Describes the behaviour of the Rocket sprite that spawns at the bottom of each level. 
/// When the player touches the rocket, the level is supposed to end.
/// </summary>
public class Rocket : MonoBehaviour
{
    [Header("Inscribed")]
    GameObject rocketPrefab;

    // text object to keep track of follower count
    public GameObject followerCount;
    // tweakable number of followers the player must have in order to win the level
    public int requiredFollowersToWin;

    /// <summary>
    /// when colliding with the player, end the level. if the player met or exceeded required follower count, they win. if they don't, they lose. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (followerCount.GetComponent<FollowerCount>().followerCount >= requiredFollowersToWin)
            {
                SceneManager.LoadScene("You_Win");
            } else{
                SceneManager.LoadScene("GameOverNotEnoughFollowers");
            }
        }
    }
}
