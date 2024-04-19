using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool debugOn;

    [Header("Inscribed")]
    public GameObject platformSquarePrefab;
    public GameObject[] prefabPlatforms;

    public GameObject Normal_Platform_MiddlePrefab;
    public GameObject Normal_Platform_LeftPrefab;
    public GameObject Normal_Platform_RightPrefab;

    // must be a negative number if we want it off-screen
    public float distanceFromCameraBottom = -0.1f;

    // spawn delay, in seconds
    public float spawnDelay = 1f;

    //speed that platforms will move upward
    public float platformSpeed = 2f;

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
        int type = Random.Range(0, prefabPlatforms.Length - 1);

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

            // First iteration spawn the left edge of the platform
            if (i == 0)
            {
                GameObject squareLeft = Instantiate<GameObject>(Normal_Platform_LeftPrefab);
                squareLeft.transform.position = position;
                squareLeft.transform.SetParent(platform.transform);

                // Last iteration spawn the right edge of the platform
            }
            else if (i == (length - 1))
            {
                GameObject squareRight = Instantiate<GameObject>(Normal_Platform_RightPrefab);
                squareRight.transform.position = position;
                squareRight.transform.SetParent(platform.transform);

                // Else spawn middle piece of the platform
            }
            else
            {
                GameObject squareMiddle = Instantiate<GameObject>(Normal_Platform_MiddlePrefab);
                squareMiddle.transform.position = position;

                squareMiddle.transform.SetParent(platform.transform);
            }

            // next square will be to the right by 1, so increase x by 1
            x++;


            //attach movement script so platform moves up
            platform.AddComponent<MoveObject>();
            platform.GetComponent<MoveObject>().speed = platformSpeed;

            if (debugOn) Debug.Log("Spawned new platform at (" + initPos.x + ", " + initPos.y + ")");

        }
    }
}
