using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu defines methods that buttons on the main menu will call when clicked. 
/// </summary>

public class MainMenu : MonoBehaviour
{
    public GameObject storyTextBox;
    public GameObject preGameTextBox;

    /// <summary>
    /// Shows the text box containing information about the current mission that is about to play, including basic instructions.
    /// </summary>
    public void ShowPreGameInfo()
    {
        preGameTextBox.SetActive(true);
    }

    /// <summary>
    /// Opens the Prototype scene, where the game will immediately play. 
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    /// <summary>
    /// Opens the text window describing some basic story along with basic instructions. 
    /// </summary>
    public void ShowInstructions()
    {
        storyTextBox.SetActive(true);
    }

    /// <summary>
    /// Hides the instructions window. 
    /// </summary>
    public void HideInstructions()
    {
        storyTextBox.SetActive(false);
    }
}
