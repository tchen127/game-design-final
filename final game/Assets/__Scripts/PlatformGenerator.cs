using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformSquarePrefab;

    // must be a negative number if we want it off-screen
    public float distanceFromCameraBottom = -0.1f;

    // spawn delay, in seconds
    public float spawnDelay = 1f;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // spawn a platform every spawnDelay seconds
        time += Time.deltaTime;
        while (time >= spawnDelay)
        {
            SpawnPlatform();
            time -= spawnDelay;
        }
    }

    void SpawnPlatform()
    {

        // first, randomize platform type. implement later once we have different types of platforms

        // then, randomize x position (between 0 and 1, 0 being leftmost edge of camera and 1 being rightmost edge of camera); y will always be a given distance 
        Vector3 initPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), distanceFromCameraBottom, 1));

        // get the real float values of x and y coords
        float x = initPos.x;
        float y = initPos.y;

        // finally, randomize length of platform BASED ON PLATFORM TYPE(e.g. length 3 with 3 squares), not yet implemented
        // current values are placeholders for now. later will be Random.Range(type.minLength, type.maxLength) or something like that maybe? idk
        int length = Random.Range(2, 8);

        // create parent Platform gameobject
        GameObject platform = new GameObject();
        platform.transform.position = new Vector2(x, y);

        // for the length of the platform, spawn a block (done so by appending to the right)
        for (int i = 0; i < length; i++)
        {

            // spawn platform and set position
            Vector2 position = new Vector2(x, y);
            GameObject square = Instantiate<GameObject>(platformSquarePrefab);
            square.transform.position = position;

            square.transform.SetParent(platform.transform);

            // next square will be to the right by 1, so increase x by 1
            x++;

        }

        Debug.Log("Spawned new platform at (" + initPos.x + ", " + initPos.y + ")");

    }
}
