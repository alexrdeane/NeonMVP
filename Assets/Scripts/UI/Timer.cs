using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float targetTime = 80f;
    public bool isRacing;
    Text txt;

    void Awake()
    {
        txt = GetComponent<Text>();
    }

    void Start()
    {
        isRacing = true;
    }

    void Update()
    {
        if (isRacing)
            txt.text = targetTime.ToString("#0");
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    public void timerEnded()
    /*{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Action of Closing the Game in Unity
#endif
        Application.Quit(); // Function of Closing the Game
    }*/
    {
        SceneManager.LoadScene(0);
    }
}