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

    [Header("Follower Movement")]
    //how far away the follower will stay from the player
    [SerializeField] public float followDistance;

    //speed at which follower will move to follow player
    [SerializeField] private float moveSpeed;

    //speed at which follower will float up until player get close enough for following
    [SerializeField] private float floatUpwardSpeed;

    [Header("Jumping")]
    //upward velocity added to followers rigidbody when jumping (how high follower will jump)
    [SerializeField] private float jumpSpeed;
    //physics layers that the raycast (used for jumping) will detect. should be set to "platform"
    [SerializeField] private LayerMask layerMask;

    //height follower can be above platform before being able to jump
    [SerializeField] private float raycastLength;

    [Header("Debugging")]
    [SerializeField] private bool debugOn;



    // how long the follower can remain off-screen before it dies
    public float defaultDeathTimer = 3f;
    private float deathTimer;

    // reference to Follower Count object (text object), which will hold the value for total follower count
    public GameObject followerCount;

    // the y position that is at the bottom of the camera
    public float bottomY;
    // y position at the top of the camera
    public float topY;


    /////////////////////////////// Non-tunable variables used for follower movement//////////////////////////////////////////////////

    //false until player gets close enough to follower, then stays true
    private bool followingPlayer = false;

    //will hold vector from follower to player, updated in FixedUpdate
    private Vector2 vecToPlayer;

    //hit2D object for raycast down from player to detect whether isGrounded
    RaycastHit2D hit2D;

    //used to detect whether player is on an edge or not
    //leftEdgeDetector is on the leftmost part of the follower's hitbox
    RaycastHit2D leftEdgeDetector;
    //rightEdgeDetector is on the rightmost part of the follower's hitbox
    RaycastHit2D rightEdgeDetector;

    //true if follower is on a platform or on the ground, used to determine if follower can jump
    private bool isGrounded;

    //width of follower hitbox
    private float followerWidth;




    void Awake()
    {
        //get the RigidBody2D for this GameObject
        rb = this.GetComponent<Rigidbody2D>();

        //get animator for this GameObject
        anim = this.GetComponent<Animator>();
        //make follower idle until collected by player
        anim.speed = 0;

        //attach the followerCount text object to this follower
        followerCount = GameObject.FindGameObjectWithTag("Follower Count");

        //only set isGrounded to true when follower is in contact with the top of a platform
        isGrounded = false;

        //get width of the follower
        followerWidth = GetComponent<BoxCollider2D>().size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = defaultDeathTimer;

        //get transform of player
        player = GameObject.FindGameObjectWithTag("Player").transform;


        //coordinates for bottom of camera
        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1));
        bottomY = bottom.y;

        //coordinates for top of camera
        Vector3 top = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
        topY = top.y;
    }


    void FixedUpdate()
    {
        //get the vector from the Follower to the player
        vecToPlayer = player.position - transform.position;

        //Make follower follow player if player gets close enough
        if (followingPlayer == false)
        {
            if (transform.position.y < bottomY)
            {
                //move object up at a constant speed
                FloatUpward();
            }

            //make follower start following player if player gets close enough
            if (Math.Abs(vecToPlayer.magnitude) < 1)
            {
                followingPlayer = true;
                followerCount.GetComponent<FollowerCount>().IncrementFollowerCount();
                //if (debugOn) Debug.Log("following player");

            }
        }

        //follow player as long as followingPlayer == true
        else
        {
            //if (debugOn) Debug.Log("Following Player");
            CheckIfOffScreen();
            FollowPlayer();

        }

        //determine if player is on a jumpable layer object.
        hit2D = Physics2D.Raycast(gameObject.transform.position, Vector2.down, raycastLength, layerMask);
        if (debugOn) Debug.DrawLine(gameObject.transform.position, gameObject.transform.position - new Vector3(0, raycastLength, 0), Color.blue);

        //isGrounded will be true if hit2D.collider is not null, otherwise it will be false
        if (hit2D.collider != null) isGrounded = true;
        else isGrounded = false;

        Debug.Log("isGrounded " + isGrounded);
    }

    private void FollowPlayer()
    {

        //follower will only control movement in x direction, so y component = 0
        vecToPlayer = player.position - transform.position;

        //animation should not be playing until follower starts to move
        anim.speed = 0;

        //only move Follower if it is far enough away
        //don't move Follower if it is below player (if follower gets ahead, player can catch up)
        if ((Math.Abs(vecToPlayer.x) > followDistance) && (vecToPlayer.y <= 5))
        {
            //move Follower towards the player
            vecToPlayer.y = 0;

            //if player is noticeably below follower, check to see if follower is stuck on an edge
            if (player.position.y < transform.position.y - 3)
            {
                switch (onEdge())
                {
                    case -1:
                        break;
                    case 0:
                        //move left
                        vecToPlayer = Vector2.left;
                        break;
                    case 1:
                        //move right
                        vecToPlayer = Vector2.right;
                        break;
                }
            }
            moveCharacter(vecToPlayer);
            AnimateFollower(vecToPlayer);
        }

        //try to jump back onto screen if below screen
        if (transform.position.y < bottomY)
        {
            Jump();
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

    private int onEdge()
    {
        //raycast down from the left and right sides of the follower's hitbox
        leftEdgeDetector = Physics2D.Raycast(new Vector2(transform.position.x - followerWidth, transform.position.y), Vector2.down, 2);
        rightEdgeDetector = Physics2D.Raycast(new Vector2(transform.position.x + followerWidth, transform.position.y), Vector2.down, 2);

        //show the edge detector rays
        Debug.DrawRay(new Vector2(transform.position.x - followerWidth, transform.position.y), Vector2.down, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + followerWidth, transform.position.y), Vector2.down, Color.green);

        bool fullyOnPlatform = (leftEdgeDetector.collider == null) && (rightEdgeDetector.collider == null);
        bool fullyOffPlatform = !(leftEdgeDetector.collider == null) && !(rightEdgeDetector.collider == null);

        //if neither is null (not on edge) return -1
        if (fullyOnPlatform || fullyOffPlatform) return -1;
        //if left detector is null, return 0 (by the animation convention, 0 refers to facing left)
        else if (leftEdgeDetector.collider == null) return 0;
        //otherwise, must be the right side that is off the platform
        else return 1;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = rb.velocity + new Vector2(0, jumpSpeed);
        }
    }

    private void AnimateFollower(Vector2 direction)
    {
        if (direction.x > 0) anim.Play("NPC_Walking_1");
        else if (direction.x < 0) anim.Play("NPC_Walking_0");
        anim.speed = 1;
    }

    // checks if the follower is off the bottom or top of the screen. if it is, keep track of a timer that, if exceeded, kills the follower
    private void CheckIfOffScreen()
    {
        if (transform.position.y < bottomY || transform.position.y > topY)
        {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0f)
            {
                Die();
            }
        }
        else
        {
            // reset death timer when on screen again
            deathTimer = defaultDeathTimer;
        }
        //Debug.Log(deathTimer);
    }

    // destroy the follower gameobject, then decrement the global follower count
    private void Die()
    {
        Destroy(gameObject);
        followerCount.GetComponent<FollowerCount>().DecrementFollowerCount();
    }


}
