using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject textBox;

    // void Start()
    // {
    //     textBox.SetActive(false);
    // }

    public void Play()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void ShowInstructions()
    {
        textBox.SetActive(true);
    }

    public void HideInstructions()
    {
        textBox.SetActive(false);
    }
}
