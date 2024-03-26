using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;


    [Header("Inscribed")]
    [SerializeField] private Transform player;
    public float followDistance;
    public float moveSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        //get the RigidBody2D for this GameObject
        rb = this.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        //get the vector from the Follower to the player
        Vector2 vecToPlayer = player.position - transform.position;
        //follower will only control movement in x direction, so y component = 0
        
        //only move Follower if it is far enough away
        //don't move Follower if it is below player (if follower gets ahead, player can catch up)
        if ((vecToPlayer.magnitude > followDistance) && (vecToPlayer.y <= .5)){
            //move Follower towards the player
            vecToPlayer.y = 0;
            moveCharacter(vecToPlayer);
        }
        
    }

    private void moveCharacter(Vector2 direction){
        //normalize so movement speed stays consistent at any distance
        direction.Normalize();

        //move Follower towards player
        Vector2 position = transform.position;
        position.x = position.x + (direction.x * moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
