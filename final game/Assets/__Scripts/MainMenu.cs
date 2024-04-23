using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject storyTextBox;
    public GameObject preGameTextBox;

    // void Start()
    // {
    //     textBox.SetActive(false);
    // }

    public void ShowPreGameInfo()
    {
        preGameTextBox.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void ShowInstructions()
    {
        storyTextBox.SetActive(true);
    }

    public void HideInstructions()
    {
        storyTextBox.SetActive(false);
    }
}
