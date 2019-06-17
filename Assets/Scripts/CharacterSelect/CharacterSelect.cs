using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject OnMenuPlayer1;
    public GameObject OnMenuPlayer2;
    public GameObject PrefabPlayer1;
    public GameObject PrefabPlayer2;

    public void Play()
    {
        if (OnMenuPlayer1.activeSelf == true)
        {
            SelectCharacter.chosenCharacter = PrefabPlayer1;
            SceneManager.LoadScene("Game");
        }

        else if (OnMenuPlayer2.activeSelf == true)
        {
            SelectCharacter.chosenCharacter = PrefabPlayer2;
            SceneManager.LoadScene("Game");
        }
    }
}