using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowerCount : MonoBehaviour
{
    public int followerCount; // The variable to display
    public TMP_Text text; // The TextMeshPro object to display

    void Start(){
        followerCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Dinos: " + followerCount);
        Debug.Log("Set FollowerCount text");

    }

    public void IncrementFollowerCount()
    {
        followerCount++;
        Debug.Log("Incremented FollowerCount");

    }

    public void DecrementFollowerCount()
    {
        followerCount--;
        Debug.Log("Decremented FollowerCount");

    }
}
