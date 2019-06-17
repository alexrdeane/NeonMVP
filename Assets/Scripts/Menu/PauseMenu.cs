using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false; // Reference to GameIsPause
    public GameObject pauseMenuUI; // Reference to pauseMenuUI
    public GameObject firstSelectedGameObject;
    EventSystem m_EventSystem;

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If hit Escape button
        {
            // If is Pause then Resume
            if (GameIsPause) 
            {
                Resume();
                Camera.main.GetComponent<AudioSource>().UnPause();
            }
            else // If is Resume (Playing) then Pause
            {
                Pause();
                m_EventSystem.SetSelectedGameObject(firstSelectedGameObject);
                Camera.main.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public void Resume() // Reference to Resume (with 'Escape' and a Button)
    {
        // Function of Resume
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    void Pause() // Reference to Pause
    {
        // Function of Pause
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu() // Reference to Menu
    {
        // If you want to see the debug
        //Debug.Log("Loading menu...");
        Time.timeScale = 1f; // In case the Menu have movement
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Function of Loading the Game
    }

    public void QuitGame() // Reference to Quit
    {
        // If you want to see the debug
        //Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Action of Closing the Game in Unity
#endif
        Application.Quit(); // Function of Closing the Game
    }
}
