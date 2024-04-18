using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rb;
    
    private RaycastHit2D hit2D;
    private Vector2 jumpVec;


    [SerializeField] private float speed = 20;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float playerHeight;
    [SerializeField] private float playerWidth;
    [SerializeField] private LayerMask layerMask;

    [Header("Debug")]
    [SerializeField] private bool debugOn;


    void Start()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();

        //set player height and width based on player gameobject
        playerHeight = transform.localScale.y;
        playerWidth = transform.localScale.x;

        //set direction and magnitude of jump
        jumpVec = rb.velocity + new Vector2(0, jumpSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //determine if player is on a jumpable layer object. 
        hit2D = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f,0 ), Vector2.down, .15f, layerMask);

        //draw raycast used to detect if player can jump
        if (debugOn) Debug.DrawLine(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f,0 ), gameObject.transform.position - new Vector3(0, (playerHeight) - .1f,0 ) - new Vector3(0, .5f, 0), Color.blue);

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
        Vector2 pos = this.transform.position;


        //used to check to see if moving will make player move off the screen
        float xPos = pos.x + xAxis * speed * Time.deltaTime;
        //if updating x position will move player off screen border, don't update x position
        if (atScreenBorder(xPos)){
            transform.position = pos;
        }
        //otherwise, update position as usual
        else {
            pos.x = xPos;
            transform.position = pos;
        } 
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = true;
            if (debugOn) Debug.Log("isGrounded: " + isGrounded);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = false;
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
        Vector3 leftEdge = new Vector3 (rightEdge.x * -1, 0, 1);

        //subtract player width from boundaries. this determines how close player can get to the edge
        float maxXPos = rightEdge.x - (playerWidth / 2);
        float minXPos = leftEdge.x + (playerWidth / 2);

        //return true if player is at border, otherwise false
        if (xPos < minXPos || xPos > maxXPos){
            return true;
        }

        return false;
    }

    //start to update position, creating a new vector that will be applied to the transform
    //if the new vector is outside the boundaries of the screen, set x component back to transform.x


}
