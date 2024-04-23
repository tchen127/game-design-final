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
    //Collider2D attached to this GameObject
    private Collider2D coll;
    //minimum y position needed to activate the collider
    private float minYVal;
    //height of the rocket's collider, used to determine when to enable collider
    private float rocketHeight;

    void Start()
    {
        //disable the collider for this gameobject until it enters the screen
        coll = GetComponent<BoxCollider2D>();
        rocketHeight = coll.bounds.size.y;
        coll.enabled = false;

        //get the minimum y position needed to activate the collider
        //this is the y value at the bottom of the screen
        minYVal = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    /// <summary>
    /// Enable the rocket's collider only if it has entered the screen
    /// </summary>
    void Update()
    {
        if (transform.position.y > (minYVal - (rocketHeight/2))){
            coll.enabled = true;
        }
    }

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
            }
            else
            {
                SceneManager.LoadScene("GameOverNotEnoughFollowers");
            }
        }
    }
}
