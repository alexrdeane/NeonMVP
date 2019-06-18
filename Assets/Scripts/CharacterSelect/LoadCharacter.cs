using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    void Start()
    {
        if (SelectCharacter.chosenCharacter != null)
        {
            GameObject clone = Instantiate(SelectCharacter.chosenCharacter, new Vector3(-18f, -1.25f, -2f), Quaternion.identity);
            clone.transform.localScale = new Vector3(7f, 7f, 7f);
            SelectCharacter.chosenCharacter = null;
            SelectCharacter.Bye();
        }
    }
}