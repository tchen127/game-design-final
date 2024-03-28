using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rb;


    [SerializeField] private float speed = 20;
    [SerializeField] private float jumpForce = 100;

    void Awake()
    {
        //tells camera to follow this object
        FollowCam.POI = this.gameObject;
    }

    void Start()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check axis for horizontal movement
        float xAxis = Input.GetAxis("Horizontal");

        //change player position based on input
        Vector2 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        transform.position = pos;

        //true if jump button is pressed
        bool pressingJump = Input.GetButton("Jump");
        //jump 
        if (pressingJump && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
