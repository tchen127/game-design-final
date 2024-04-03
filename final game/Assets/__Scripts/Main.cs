using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Replace this with platform generator
public class Main : MonoBehaviour
{
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabPlatform;
    public float platformSpawnPerSecond = 0.5f;
    public float platformInsetDefault = 1.5f;

    private BoundsCheck boundCheck;
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        boundCheck = GetComponent<BoundsCheck>();

        Invoke(nameof(SpawnPlatform), 1f / platformSpawnPerSecond);
    }

    public void SpawnPlatform()
    {
        // Pick a random type of platform to instantiate 
        int ndx = Random.Range(0, prefabPlatform.Length);
        GameObject go = Instantiate<GameObject>(prefabPlatform[ndx]);

        float platformInset = platformInsetDefault;

        // set initial position of the platform 
        Vector2 pos = Vector2.zero;

        // add offset if cause problems 
        float xMin = -boundCheck.camWidth;
        float xMax = boundCheck.camWidth;

        pos.x = Random.Range(xMin, xMax);
        pos.y = boundCheck.camHeight + platformInset;
        go.transform.position = pos;

        Invoke(nameof(SpawnPlatform), 1f / platformSpawnPerSecond);
    }
}
