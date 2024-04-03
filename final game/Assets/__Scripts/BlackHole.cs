using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    public int restart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void onTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AIFollower"))
        {
            SceneManager.LoadScene(restart);
        }

    }
}
