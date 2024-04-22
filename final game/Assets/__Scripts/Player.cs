using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private Animator anim;
    public enum eMode { idle, move }
    private bool isGrounded = false;
    //rigidbody of player
    private Rigidbody2D rb;
    //width of the player in Unity units
    private float playerWidth;



    public int dirHeld = -1;
    public int facing = 1;
    public eMode mode = eMode.idle;

    [SerializeField] private float speed = 8;

    [Header("Jumping")]
    [SerializeField] private float jumpSpeed = 5;

    //length of raycast sent down from player's transform to detect the ground
    [SerializeField] private float raycastLength = 2f;
    //layermask for the raycast to detect platforms (for jumping)
    [SerializeField] private LayerMask layerMask;
    //holds result of downward raycast used for jump mechanic
    private RaycastHit2D hit2D;
    //velocity added to player when jumping (should only have a non-zero y component)
    private Vector2 jumpVec;

    [Header("Debug")]
    [SerializeField] private bool debugOn;

    void Awake()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();

        //get animator component
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //set direction and magnitude of jump
        jumpVec = rb.velocity + new Vector2(0, jumpSpeed);

        //set playerWidth
        playerWidth = transform.localScale.x;
        if (debugOn) Debug.Log("playerWidth: " + playerWidth);

    }

    //use fixedupdate for any physics interactions
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
<<<<<<< Updated upstream
        //if updating x position will move player off screen border, don't update x position
        /*if (atScreenBorder(xPos))
        {
            if (debugOn) Debug.Log("atScreenBorder");
            if (debugOn) Debug.Log("atScreenBorder");
            transform.position = pos;
        }
        */
        //otherwise, update position as usual
        //else
        //{
            pos.x = xPos;
=======

        pos.x = xPos;
        transform.position = pos;

        //don't let player go off right side of screen
        if (transform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(.95f,0,0)).x){
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(.95f,0,0)).x;
            transform.position = pos;
        }
        //don't let player go off left side of screen
        else if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(.05f,0,0)).x){
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(.05f,0,0)).x;
>>>>>>> Stashed changes
            transform.position = pos;
        //}

        if (transform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3 (1, 0, 0)).x){
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
            transform.position = pos;
        }
        else if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3 (0, 0, 0)).x){
            pos.x = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
            transform.position = pos;
        }



    }

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

    /// <summary>
    /// Return true if player is at the side of the screen
    /// Can alter width of character to get closer/farther from the border
    /// </summary>
    /// <returns>True if player is at side border of screen, otherwise false</returns>
    private bool atScreenBorder(float xPos)
    {
        //get coordinates of left and right border of screen
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 1));
        Vector3 leftEdge = new Vector3(rightEdge.x * -1, 0, 1);

        //subtract player width from boundaries. this determines how close player can get to the edge
        float maxXPos = rightEdge.x - (playerWidth / 2);
        float minXPos = leftEdge.x + (playerWidth / 2);

        //return true if player is at border, otherwise false
        if (xPos < minXPos || xPos > maxXPos)
        {
            return true;
        }

        return false;
    }
}

