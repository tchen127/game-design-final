using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;

    //animator that holds walking animations of follower
    private Animator anim;


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

    // how long the follower can remain off-screen before it dies
    public float defaultDeathTimer = 3f;
    private float deathTimer;

    // reference to Follower Count object (text object), which will hold the value for total follower count
    public GameObject followerCount;

    // the y position that is at the bottom of the camera
    public float bottomY;
    
    void Awake(){
        //get the RigidBody2D for this GameObject
        rb = this.GetComponent<Rigidbody2D>();

        //get animator for this GameObject
        anim = this.GetComponent<Animator>();

        //attach the followerCount text object to this follower
        followerCount = GameObject.FindGameObjectWithTag("Follower Count");
    }

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = defaultDeathTimer;

        //get transform of player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1));
        bottomY = bottom.y;
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
<<<<<<< Updated upstream
=======
                followerCount.GetComponent<FollowerCount>().IncrementFollowerCount();
                if (debugOn) Debug.Log("following player");
>>>>>>> Stashed changes
            }
        }

        //follow player as long as followingPlayer == true
        else
        {
<<<<<<< Updated upstream
            if (debugOn) Debug.Log("Following Player");
=======
            CheckIfOffScreen();
>>>>>>> Stashed changes
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {

        //follower will only control movement in x direction, so y component = 0
        vecToPlayer = player.position - transform.position;
        
        //animation should not be playing until follower starts to move
        anim.speed = 0;

        //only move Follower if it is far enough away
        //don't move Follower if it is below player (if follower gets ahead, player can catch up)
        if ((Math.Abs(vecToPlayer.x) > followDistance) && (vecToPlayer.y <= 1))
        {
            //move Follower towards the player
            vecToPlayer.y = 0;
            moveCharacter(vecToPlayer);
            AnimateFollower(vecToPlayer);
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

    private void AnimateFollower(Vector2 direction){
        if (direction.x > 0) anim.Play("NPC_Walking_1");
        else if (direction.x < 0) anim.Play("NPC_Walking_0");
        anim.speed = 1;
    }

    // checks if the follower is off the bottom of the screen. if it is, keep track of a timer that, if exceeded, kills the follower
    private void CheckIfOffScreen()
    {
        if (transform.position.y < bottomY)
        {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0f)
            {
                Die();
            }
        }
        else {
            // reset death timer when on screen again
            deathTimer = defaultDeathTimer;
        }
        Debug.Log(deathTimer);
    }

    // destroy the follower gameobject, then decrement the global follower count
    private void Die()
    {
        Destroy(gameObject);
        followerCount.GetComponent<FollowerCount>().DecrementFollowerCount();
    }
}
