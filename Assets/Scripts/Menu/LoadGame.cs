using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void PlayGame() // Reference to Open the Game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Function of Loading the Game
    }
}
