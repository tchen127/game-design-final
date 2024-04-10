using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{
    

    [Header("Inscribed")]
    [SerializeField] private GameObject followerPrefab;
    //number of followers to be spawned
    [SerializeField] private int followerNum = 5;
    //spawn delay, in seconds
    [SerializeField] private float spawnDelay = 1f;
    //time that level will last
    [SerializeField] private float levelTime = 20f;

    //number of followers that have been spawned
    private float currFollowers = 0;
    //time since last follower was spawned
    float timeWaited;

    // Start is called before the first frame update
    void Start()
    {
        //start counting time waited since last follower spawn (will be 0 during first frame)
        timeWaited = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWaited > spawnDelay && currFollowers < followerNum)
        {
            SpawnFollower();
            timeWaited = 0;
        }
        else timeWaited += Time.deltaTime;
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
        Debug.Log("currFollowers: " + currFollowers + "\nfollowerNum: " + followerNum);
    }

    //Program Flow
    //get random x position within screen boundary
    //instantiate follower prefab below screen w/ the random x position
    //stop instantiating followers once the follower limit has been reached
}
