using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public HealthBar health;
    public float curHealth;

    private void Awake()
    {
        curHealth = health.curHealth;
    }
 
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "HurtBox")
        {
            if (Input.GetAxis("LightAttack2") != 0)
            {
                curHealth -= 10;
            }
        }
        /*
        if (col.gameObject.tag == "Player" && (Input.GetAxis("MediumAttack") != 0))
        {
            curHealth2 -= 20;
        }
        if (col.gameObject.tag == "Player" && (Input.GetAxis("HeavyAttack") != 0))
        {
            curHealth2 -= 30;
        }
        if (col.gameObject.tag == "HurtBox" && col.gameObject.tag == "LightAttack" && (Input.GetAxis("LightAttack") != 0))
        {
            curHealth -= 10;
        }
        
        if (col.gameObject.tag == "Player" && (Input.GetAxis("MediumAttack2") != 0))
        {
            curHealth -= 20;
        }
        if (col.gameObject.tag == "Player" && (Input.GetAxis("HeavyAttack2") != 0))
        {
            curHealth -= 30;
        }
        */
    }
}
