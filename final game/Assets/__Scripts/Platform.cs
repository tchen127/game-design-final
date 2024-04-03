using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Platform : MonoBehaviour
{
    [Header("Inscribed")]
    public float moving_speed = 10f;

    private BoundsCheck boundCheck;

    void Awake()
    {
        boundCheck = GetComponent<BoundsCheck>();
    }

    public Vector2 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();

        // Check whether platform has gone above the screen, beyond blackhole
        if (!boundCheck.isOnScreen)
        {
            // !! add y position offset from camera size (change later!)
            if (pos.y >= boundCheck.camHeight)
            {
                // platform too high up, absorb by blackhole
                Destroy(gameObject);
            }
        }
    }

    public virtual void Move()
    {
        Vector2 tempPos = pos;

        // gets the current position of platform and move it ??upward?? Y direction
        tempPos.y += moving_speed * Time.deltaTime;
        pos = tempPos;
    }
}
