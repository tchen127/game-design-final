using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public enum eMode { idle, move, jump }
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private float playerHeight;
    private float playerWidth;
    private RaycastHit2D hit2D;
    private Vector2 jumpVec;

    public int dirHeld = -1;
    public int facing = 1;
    public eMode mode = eMode.idle;

    private Vector2[] directions = new Vector2[]{
        Vector2.right, Vector2.left};

    [SerializeField] private float speed = 20;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private LayerMask layerMask;

    [Header("Debug")]
    [SerializeField] private bool debugOn;

    void Start()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();

        //set direction and magnitude of jump
        jumpVec = rb.velocity + new Vector2(0, jumpSpeed);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //determine if player is on a jumpable layer object.
        hit2D = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f, 0), Vector2.down, .5f, layerMask);
        Debug.DrawLine(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f, 0), gameObject.transform.position - new Vector3(0, (playerHeight) - .1f, 0) - new Vector3(0, .5f, 0), Color.blue);

        hit2D = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f, 0), Vector2.down, .15f, layerMask);

        //draw raycast used to detect if player can jump
        if (debugOn) Debug.DrawLine(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f, 0), gameObject.transform.position - new Vector3(0, (playerHeight) - .1f, 0) - new Vector3(0, .5f, 0), Color.blue);

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
        pos.x += xAxis * speed * Time.deltaTime;
        transform.position = pos;

        //used to check to see if moving will make player move off the screen
        float xPos = pos.x + xAxis * speed * Time.deltaTime;
        //if updating x position will move player off screen border, don't update x position
        if (atScreenBorder(xPos))
        {
            transform.position = pos;
        }
        //otherwise, update position as usual
        else
        {
            pos.x = xPos;
            transform.position = pos;
        }
    }

    void Update()
    {
        if (mode == eMode.idle || mode == eMode.move)
        {
            dirHeld = -1;
            if (Input.GetKey(KeyCode.RightArrow)) dirHeld = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) dirHeld = 0;

            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                // facing = dirHeld;
                mode = eMode.move;
            }

            // Jump 
            if (Input.GetKey(KeyCode.Space))
            {
                // mode = eMode.jump;
                hit2D = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f, 0), Vector2.down, .5f, layerMask);

                if (hit2D.collider != null)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
                if (isGrounded)
                {
                    mode = eMode.jump;
                }
            }
        }

        // Act on the current mode
        switch (mode)
        {
            case eMode.idle:
                rb.velocity = new Vector2(0, -speed);
                break;

            case eMode.move:
                if (dirHeld != facing)
                {
                    transform.Rotate(0, 180, 0);
                    facing = dirHeld;
                }
                break;

            case eMode.jump:
                rb.velocity = jumpVec;
                break;

        }

    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = true;
            Debug.Log("isGrounded: " + isGrounded);

            if (debugOn) Debug.Log("isGrounded: " + isGrounded);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = false;
            Debug.Log("isGrounded: " + isGrounded);
            if (debugOn) Debug.Log("isGrounded: " + isGrounded);
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