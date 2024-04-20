using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTime : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 

    public float camWidth;
    public float camHeight;

    // Start is called before the first frame update
    void Awake()
    {
        // access to 1st camera with tag MainCamera, get size
        // height is the distance from the origin to the top or bottom edge of the screen
        camHeight = Camera.main.orthographicSize;

        // ratio defined by aspect ratio of game pane (portrait)
        camWidth = camHeight * Camera.main.aspect;
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.y < -camHeight) { 
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            if (timeLeft < 0)
            {
                //Do something useful or Load a new game scene depending on your use-case
                SceneManager.LoadScene("GameOver");
            }
        }
    }
} 
