using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoundsCheck : MonoBehaviour
{
    private float timespanTop;
    private float timespanBottom;

    // Set the amount of time for the off screen timer
    public float timeLeft = 3.0f;
    // Text for the countdown timer
    public Text startText;
    // Text for the fall alert
    public Text fallAlert;
    // Text for the black hole alert
    public Text blackHoleAlert;

    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0,
        offUp = 4,
        offDown = 8
    }

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float camWidth;
    public float camHeight;

    [Header("Black Hole Size")]
    //determines how far down the black hole reaches from the top of the screen
    [SerializeField] private float blackHoleSize;

    [Header("Debug")]
    [SerializeField] private bool debugOn;

    // Start is called before the first frame update
    void Awake()
    {
        // Set the text fields to disabled until needed
        fallAlert.enabled = false; 
        blackHoleAlert.enabled = false;
        // access to 1st camera with tag MainCamera, get size
        // height is the distance from the origin to the top or bottom edge of the screen
        camHeight = Camera.main.orthographicSize;

        // ratio defined by aspect ratio of game pane (portrait)
        camWidth = camHeight * Camera.main.aspect;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 pos = transform.position;

        // Reset values for falling or being sucked up by black hole if player escapes either scenario
        if ((pos.y < (camHeight - 7)) && (pos.y > -camHeight)) {
            // Remove the text from the screen
            if (fallAlert.enabled) {
                fallAlert.enabled = false; 
            }
            
            if (blackHoleAlert.enabled) {
                blackHoleAlert.enabled = false;
            }
            // Reset time and remove countdown from screen
            timeLeft = 3.0f;
            startText.text = " ";
        }

        // Logic for detecting whether player spends too
        // much time close too close to black hole
        if (debugOn) Debug.Log("CAM HEIGHT" + (camHeight - 5).ToString("0"));
        if (pos.y > (camHeight - blackHoleSize)) {
            if (debugOn) Debug.Log(camHeight - blackHoleSize);
            blackHoleAlert.enabled = true;
            // Subtract time and update onscreen timer
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            // When time reaches zero load game over scene
            if (timeLeft < 0) {
                SceneManager.LoadScene("GameOverBlackHole");
            }
        }

        // Logic for detecting player off screen at bottom for x amount of time
        if (pos.y < -camHeight) {
            fallAlert.enabled = true;
            if (debugOn) Debug.Log("Below Camera Bottom");
            // Subtract time and update onscreen timer
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            // When time reaches zero load game over scene
            if (timeLeft < 0) {
                SceneManager.LoadScene("GameOver");
            }
        }

    }

    public bool isOnScreen
    {
        get { return (screenLocs == eScreenLocs.onScreen); }
    }

}
