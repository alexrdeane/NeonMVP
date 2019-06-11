using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Animator[] anim;
    public CapsuleCollider punchCol;

    void Start()
    {
        punchCol.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim[0].SetBool("isPunch", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim[0].SetBool("isPunch", false);
        }
    }

    void PunchActivate()
    {
        punchCol.enabled = true;
    }

    void PunchDeactivate()
    {
        punchCol.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //set target to the thing in zone
            //switch state to seek
            Destroy(other);
        }
    }
}
