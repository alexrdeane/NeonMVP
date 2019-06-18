using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame() // Reference to Open the Game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Function of Loading the Game
    }

    public void QuitGame() // Reference to Close the Game

    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Action of Closing the Game in Unity
#endif
        Application.Quit(); // Function of Closing the Game
    }
}