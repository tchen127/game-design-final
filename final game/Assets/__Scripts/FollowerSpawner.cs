using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{


    [Header("Inscribed")]
    [SerializeField] private GameObject followerPrefab;
    //number of followers to be spawned
    [SerializeField] private int maxFollowerNum = 5;
    //spawn delay, in seconds
    [SerializeField] private float spawnDelay = 1f;
    //time that level will last
    [SerializeField] private float stopSpawningTime = 20f;

    //number of followers that have been spawned
    private float currFollowers = 0;
    //time since last follower was spawned
    float timeWaited;
    //time that the level has been running. set to 0 in start method
    float levelTime;

    // Start is called before the first frame update
    void Start()
    {   
        //time that level has been running
        levelTime = 0;

        //start counting time waited since last follower spawn (will be 0 during first frame)
        timeWaited = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //spawn followers will under maxFollowerNum and until levelTime runs out
        if (currFollowers < maxFollowerNum && Time.time < stopSpawningTime)
        {
            if (timeWaited > spawnDelay && currFollowers < maxFollowerNum)
            {
                SpawnFollower();
                //reset time waited so that next follower will spawn in  spawnDelay seconds
                timeWaited = 0;
                //increment follower count
                currFollowers++;
            }
            else timeWaited += Time.deltaTime;
        }
    }

    private void SpawnFollower()
    {
        //get random position that will be used to place the follower
        Vector3 initPos = Camera.main.ViewportToWorldPoint(new UnityEngine.Vector3(Random.Range(0f, 1f), -0.1f, 1));
        //get x and y position of initPosition
        float x = initPos.x;
        float y = initPos.y;

        //instantiate a follower
        GameObject follower = Instantiate<GameObject>(followerPrefab);
        //set position of follower
        follower.transform.position = new Vector2(x, y);
        //increment currFollowers
        currFollowers++;

    }
}
