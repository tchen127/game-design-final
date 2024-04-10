using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private float playerHeight;
    private RaycastHit2D hit2D;
    private Vector2 jumpVec;


    [SerializeField] private float speed = 20;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        //tells camera to follow this object
        ScrollingCamera.POI = this.gameObject;
    }

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
}
