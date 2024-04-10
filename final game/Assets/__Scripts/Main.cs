using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Replace this with platform generator
public class Main : MonoBehaviour
{

    [Header("Inscribed")]
    //time that the level will last
    [SerializeField] private float levelTime;

    [Header("PlatformSpawning")]
    [SerializeField] private GameObject[] prefabPlatform;
    [SerializeField] private float spawnDistanceFromCameraBottom;
    [SerializeField] private float platformSpawnPerSecond = 1f;

    [Header("AI Spawning")]
    [SerializeField] private int maxFollowerCount;

    private float time;

    void Start()
    {
        time = 0;
    }

    void Update()
    {
        // Pick a random type of platform to instantiate 
        int ndx = Random.Range(0, prefabPlatform.Length);

        // set initial position of the platform 
        Vector3 initPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), spawnDistanceFromCameraBottom, 1));

    }
}
