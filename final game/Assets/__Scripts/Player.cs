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
        hit2D = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f,0 ), Vector2.down, .5f, layerMask);
        Debug.DrawLine(gameObject.transform.position - new Vector3(0, (playerHeight / 2) - .1f,0 ), gameObject.transform.position - new Vector3(0, (playerHeight) - .1f,0 ) - new Vector3(0, .5f, 0), Color.blue);
        //isGrounded will be true if hit2D.collider is not null, otherwise it will be false
        if (hit2D.collider != null) isGrounded = true;
        else isGrounded = false;
        Debug.Log(isGrounded);

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

        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = true;
            Debug.Log("isGrounded: " + isGrounded);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = false;
            Debug.Log("isGrounded: " + isGrounded);
        }
    }

    /// <summary>
    /// Return true if player is at the side of the screen
    /// Can alter width of character to get closer/farther from the border
    /// </summary>
    /// <returns>True if player is at side border of screen, otherwise false</returns>
    private bool atSideOfScreen()
    {
        //get coordinates of left and right border of screen
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 1));
        Vector3 leftEdge = new Vector3 (rightEdge.x * -1, 0, 1);

        //subtract player width from boundaries. this determines how close player can get to the edge
        float maxXPos = rightEdge.x - (playerWidth / 2);
        float minXPos = leftEdge.x + (playerWidth / 2);

        //player position
        Vector3 pos = this.transform.position;

        //return true if player is at border, otherwise false
        if (pos.x < minXPos || pos.x > maxXPos){
            return true;
        }

        return false;
    }
}
