using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Class <c>Player<C> defines the logic for player movement and animation
/// </summary>
public class Player : MonoBehaviour
{
    //animator attached to the player
    private Animator anim;

    //true if player is on a platform, otherwise false
    private bool isGrounded = false;

    //rigidbody of player
    private Rigidbody2D rb;

    //width of the player in Unity units
    private float playerWidth;

    //direction of horizontal input axis
    public int dirHeld = -1;

    //direction player is facing (1 is right, 0 is left)
    public int facing = 1;

    //speed of horizontal player movement
    [SerializeField] private float speed = 8;

    [Header("Jumping")]
    //upward velocity of vector applied to player for jumping (determines how high player jumps)
    [SerializeField] private float jumpSpeed = 5;

    //velocity added to player when jumping (only the y component should be nonzero)
    private Vector2 jumpVec;

    //length of raycast sent down from player's transform to detect the ground
    [SerializeField] private float raycastLength = 2f;

    //layermask for the raycast to detect platforms (for jumping)
    [SerializeField] private LayerMask layerMask;

    //holds result of downward raycast used for jump mechanic
    private RaycastHit2D hit2D;

    

    [Header("Debug")]
    //true if you want debug messages to show
    [SerializeField] private bool debugOn;

    /// <summary>
    /// Get rigidbody and animator components attached to the player
    /// </summary>
    void Awake()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();

        //get animator component
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Initialize jumpVec and playerWidth 
    /// </summary>
    void Start()
    {
        //set direction and magnitude of jump
        jumpVec = rb.velocity + new Vector2(0, jumpSpeed);

        //set playerWidth
        playerWidth = transform.localScale.x;
        if (debugOn) Debug.Log("playerWidth: " + playerWidth);

    }

    /// <summary>
    /// Handles all physics/movement interactions.
    /// Determines if player can jump. Also applies movement based on input. 
    /// </summary>
    void FixedUpdate()
    {
        //determine if player is on a jumpable layer object.
        hit2D = Physics2D.Raycast(gameObject.transform.position, Vector2.down, raycastLength, layerMask);

        if (debugOn) Debug.DrawLine(gameObject.transform.position, gameObject.transform.position - new Vector3(0, raycastLength, 0), Color.blue);


        //isGrounded will be true if hit2D.collider is not null, otherwise it will be false
        if (hit2D.collider != null) isGrounded = true;
        else isGrounded = false;

        //true if jump button is pressed
        bool pressingJump = Input.GetButton("Jump");
        //jump 
        if (pressingJump && isGrounded)
        {
            rb.velocity = jumpVec;
        }

        //check axis for horizontal movement
        float xAxis = Input.GetAxis("Horizontal");

        //change player position based on input
        Vector2 pos = transform.position;

        //used to check to see if moving will make player move off the screen
        float xPos = pos.x + xAxis * speed * Time.deltaTime;

        pos.x = xPos;
        transform.position = pos;

        //don't let player go off right side of screen
        if (transform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(.95f, 0, 0)).x)
        {
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(.95f, 0, 0)).x;
            transform.position = pos;
        }
        //don't let player go off left side of screen
        else if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(.05f, 0, 0)).x)
        {
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(.05f, 0, 0)).x;
            transform.position = pos;
        }

    }

    /// <summary>
    /// Animate player based on direction of input
    /// </summary>
    void Update()
    {

        //dirHeld will be -1 if there is no input
        dirHeld = -1;
        //make dirHeld = 1 if input is to the right
        if (Input.GetKey(KeyCode.RightArrow)) dirHeld = 1;
        //make dirHeld = 0 if input is to the left
        if (Input.GetKey(KeyCode.LeftArrow)) dirHeld = 0;

        //Animation
        if (dirHeld == -1)
        {
            anim.speed = 0;
        }
        else
        {
            anim.Play("Sten_Walk_" + dirHeld);
            anim.speed = 1;
        }

    }
}


