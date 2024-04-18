using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;


    [Header("Inscribed")]
    [SerializeField] private Transform player;

    //how far away the follower will stay from the player
    [SerializeField] public float followDistance;

    //speed at which follower will move to follow player
    [SerializeField] private float moveSpeed;

    //speed at which follower will float up until player get close enough for following
    [SerializeField] private float floatUpwardSpeed;

    [Header("Debugging")]
    [SerializeField] private bool debugOn;

    //false until player gets close enough to follower, then stays true
    private bool followingPlayer = false;

    //will hold vector from follower to player, updated in FixedUpdate
    private Vector2 vecToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        //get the RigidBody2D for this GameObject
        rb = this.GetComponent<Rigidbody2D>();

        //get transform of player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //get the vector from the Follower to the player
        vecToPlayer = player.position - transform.position;

        //Make follower follow player if player gets close enough
        if (followingPlayer == false)
        {
            //move object up at a constant speed
            FloatUpward();

            //make follower start following player if player gets close enough
            if (Math.Abs(vecToPlayer.magnitude) < 1)
            {
                followingPlayer = true;
            }
        }

        //follow player as long as followingPlayer == true
        else
        {
            Debug.Log("Following Player");
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {

        //follower will only control movement in x direction, so y component = 0
        vecToPlayer = player.position - transform.position;

        //only move Follower if it is far enough away
        //don't move Follower if it is below player (if follower gets ahead, player can catch up)
        if ((Math.Abs(vecToPlayer.x) > followDistance) && (vecToPlayer.y <= 1))
        {
            //move Follower towards the player
            vecToPlayer.y = 0;
            moveCharacter(vecToPlayer);
        }
    }

    private void FloatUpward()
    {
        //move object up at a constant speed
        rb.velocity = new Vector2(0, floatUpwardSpeed);
    }

    private void moveCharacter(Vector2 direction)
    {
        //normalize so movement speed stays consistent at any distance
        direction.Normalize();

        //move Follower towards player
        Vector2 position = transform.position;
        position.x = position.x + (direction.x * moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
