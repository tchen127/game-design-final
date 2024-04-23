using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The PlatformGenerator script handles the logic behind platform spawning, while the level is being played.
/// In general, it does as follows:
/// - every x seconds, it will choose a random x position to spawn a platform
/// - it will randomly choose what platform type to spawn (for now, only normal or crumble)
/// - it will randomly choose between two integers how many units long the platform will be
/// - it will spawn the platform slightly off the bottom of the screen, and it will slowly move upwards
/// </summary>
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

    public GameObject Crumble_Platform_MiddlePrefab;
    public GameObject Crumble_Platform_LeftPrefab;
    public GameObject Crumble_Platform_RightPrefab;

    // must be a negative number if we want it off-screen
    public float distanceFromCameraBottom = -0.1f;

    // spawn delay, in seconds
    public float spawnDelay = 1f;

    //speed that platforms will move upward
    public float platformSpeed = 2f;

    // to track time passed since beginning of play
    private float time;

    /// <summary>
    /// Initializes time to 0, to later use to time spawns
    /// </summary>
    void Start()
    {
        time = 0f;
    }

    /// <summary>
    /// Uses the time variable to spawn a platform every specified number of seconds (spawnDelay)
    /// </summary>
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

    /// <summary>
    /// Spawns a single platform. One platform is made up of square units and is generated according to: 
    /// - randomly determined x position
    /// - randomly determined platform type
    /// - randomly determined length (number of square units)
    /// </summary>
    void SpawnPlatform()
    {
        // first, randomize platform type. type == 1 means a normal platform, any other number generated means a crumble platform
        int type = Random.Range(0, 2);

        // then, randomize x position (between 0 and 1, 0 being leftmost edge of camera and 1 being rightmost edge of camera); y will always be a given distance according to distanceFromCameraBottom
        Vector3 initPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), distanceFromCameraBottom, 1));

        // get the real float values of x and y coords
        float x = initPos.x;
        float y = initPos.y;

        // finally, randomize length of platform BASED ON PLATFORM TYPE(e.g. length 3 with 3 squares), not yet implemented
        // current values are placeholders for now. later will be Random.Range(type.minLength, type.maxLength) or something like that maybe? idk
        int length = Random.Range(4, 10);

        // create parent Platform gameobject
        GameObject platform = new GameObject();
        platform.transform.position = new Vector2(x, y);

        // now, spawn platform based on platform type
        if (type == 1)
        {
            // for the length of the platform, spawn a block 
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
            }
        }
        else // spawn a crumble platform
        {
            for (int i = 0; i < length; i++)
            {
                // spawn platform and set position
                Vector2 position = new Vector2(x, y);

                // First iteration spawn the left edge of the platform
                if (i == 0)
                {
                    GameObject squareLeft = Instantiate<GameObject>(Crumble_Platform_LeftPrefab);
                    squareLeft.transform.position = position;
                    squareLeft.transform.SetParent(platform.transform);

                    // Last iteration spawn the right edge of the platform
                }
                else if (i == (length - 1))
                {
                    GameObject squareRight = Instantiate<GameObject>(Crumble_Platform_RightPrefab);
                    squareRight.transform.position = position;
                    squareRight.transform.SetParent(platform.transform);

                    // Else spawn middle piece of the platform
                }
                else
                {
                    GameObject squareMiddle = Instantiate<GameObject>(Crumble_Platform_MiddlePrefab);
                    squareMiddle.transform.position = position;
                    squareMiddle.transform.SetParent(platform.transform);
                }
                // next square will be to the right by 1, so increase x by 1
                x++;
            }

            // Assign crumble-related components.
            // crumble mom is the parent of all crumble square units
            platform.AddComponent<CrumbleMom>();
            // rigidbody is required for collision detection
            platform.AddComponent<Rigidbody2D>();
            platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //attach movement script so platform moves up
        platform.AddComponent<MoveObject>();
        platform.GetComponent<MoveObject>().speed = platformSpeed;

        if (debugOn) Debug.Log("Spawned new platform at (" + initPos.x + ", " + initPos.y + ")");

    }
}
