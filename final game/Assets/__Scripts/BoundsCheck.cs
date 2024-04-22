using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoundsCheck : MonoBehaviour
{
    private float eventTimeBottom = 0;
    private float eventTimeTop = 0;
    private float timespanTop;
    private float timespanBottom;

    public float timeLeft = 3.0f;
    public Text startText;

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
        // access to 1st camera with tag MainCamera, get size
        // height is the distance from the origin to the top or bottom edge of the screen
        camHeight = Camera.main.orthographicSize;

        // ratio defined by aspect ratio of game pane (portrait)
        camWidth = camHeight * Camera.main.aspect;

    }
/*
    void update() {
        Vector2 pos = transform.position;

        if (pos.y < -camHeight) {
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            if (timeLeft < 0) {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
*/
    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 pos = transform.position;

        screenLocs = eScreenLocs.onScreen;

        // !! change offset to make platform spawn a little off the x axis
        // Restrict the X position to camWidth and some offset later
        if (pos.x > camWidth)
        {
            timeLeft = 3.0f;
            pos.x = camWidth;                                                // e
        }
        if (pos.x < -camWidth)
        {
            pos.x = -camWidth;                                                // e
        }

        // Restrict the Y position to camHeight
        if (pos.y > camHeight)

        {
            //pos.y = camHeight;
            screenLocs |= eScreenLocs.offUp;                                                // e
        }
        if (pos.y < -camHeight)
        {
            //pos.y = -camHeight;
            screenLocs |= eScreenLocs.offDown;                                          // e
        }

        // no keepOnScreen function
        if (!isOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
        }

        if ((pos.y < (camHeight - 7)) && (pos.y > -camHeight)) {
            if (debugOn) Debug.Log("HERE");
            timeLeft = 3.0f;
            startText.text = " ";
        }

        // Logic for detecting whether player spends too
        // much time close too close to black hole

        if (debugOn) Debug.Log("CAM HEIGHT" + (camHeight - 5).ToString("0"));
        if (pos.y > (camHeight - blackHoleSize)) {
            if (debugOn) Debug.Log(camHeight - blackHoleSize);
            startText.text = "Black Hole Power is too STRONG!";
            // Subtrack time and update onscreen timer
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            // When time reaches zero load game over scene
            if (timeLeft < 0) {
                SceneManager.LoadScene("GameOverBlackHole");
            }
            /*                                         // e
            if (eventTimeTop == 0) {
                eventTimeTop = Time.time;
                timespanTop = eventTimeTop + 3;
            }

            if (timespanTop < Time.time & pos.y > (camHeight - 5)){
                SceneManager.LoadScene("GameOverBlackHole");
                if (debugOn) Debug.Log("BLACK HOLE GOT YA");
            }
            */
            //pos.y = camHeight;                                                // e
        }

        // Logic for detecting player off screen at bottom for x amount of time
        if (pos.y < -camHeight) {
            startText.text = "Falling!";
            if (debugOn) Debug.Log("Below Camera Bottom");
            // Subtrack time and update onscreen timer
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

    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }
}
