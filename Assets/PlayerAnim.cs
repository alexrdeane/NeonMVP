using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("isPunching", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("isPunching", false);
        }
    }
}
