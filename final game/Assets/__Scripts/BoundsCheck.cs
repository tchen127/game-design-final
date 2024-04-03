using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Awake()
    {
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

        screenLocs = eScreenLocs.onScreen;

        // !! change offset to make platform spawn a little off the x axis
        // Restrict the X position to camWidth and some offset later
        if (pos.x > camWidth)
        {
            pos.x = camWidth;                                                // e
        }
        if (pos.x < -camWidth)
        {
            pos.x = -camWidth;                                                // e
        }

        // Restrict the Y position to camHeight
        if (pos.y > camHeight)
        {
            pos.y = camHeight;
            screenLocs |= eScreenLocs.offUp;                                                // e
        }
        if (pos.y < -camHeight)
        {
            pos.y = -camHeight;
            screenLocs |= eScreenLocs.offDown;                                          // e
        }

        // no keepOnScreen function
        if (!isOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
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