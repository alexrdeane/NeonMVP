using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public static GameObject chosenCharacter;
    public static GameObject self;

    void Awake()
    {
        self = this.gameObject;
        DontDestroyOnLoad(this);
    }

    public static void Bye()
    {
        Destroy(self);
    }
}