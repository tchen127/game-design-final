using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowerCount : MonoBehaviour
{
    public int followerCount = 0; // The variable to display
    public TMP_Text text; // The TextMeshPro object to display

    // Update is called once per frame
    void Update()
    {
        text.SetText("Dinos: " + followerCount);
    }

    public void IncrementFollowerCount()
    {
        followerCount++;
    }

    public void DecrementFollowerCount()
    {
        followerCount--;
    }
}
