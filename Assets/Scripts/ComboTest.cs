using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTest : MonoBehaviour
{
    public float keyTime = 2f;
    public float time = 0f;
    public int lightKeyPress;
    public int mediumKeyPress;
    public int heavyKeyPress;
    public static float blockEffectiveness = 2f;
    public static float blockTime = 2f;
    public static bool isBlocking = false;
    public float damage;
    public float health;


    void Update()
    {
        time -= Time.deltaTime;
        time = Mathf.Clamp(time, 0, keyTime);
        blockEffectiveness -= Time.deltaTime;
        blockEffectiveness = Mathf.Clamp(blockEffectiveness, 0, blockTime);
        if (Input.anyKey)
        {
            Attack();
        }
        if (time == 0)
        {
            lightKeyPress = 0;
            mediumKeyPress = 0;
            heavyKeyPress = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBlocking = true;
            blockEffectiveness = blockTime;

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isBlocking = false;
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) && time >= 0)
        {
            lightKeyPress += 1;
            if (Input.GetKeyDown(KeyCode.A) && lightKeyPress >= 3)
            {
                lightKeyPress = 0;

                Debug.Log("Light Combo");
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && time == 0)
        {
            time = keyTime;

            Debug.Log("Light Punch");
        }

        if (Input.GetKeyDown(KeyCode.S) && time >= 0)
        {
            mediumKeyPress += 1;
            if (Input.GetKeyDown(KeyCode.S) && mediumKeyPress >= 3)
            {
                mediumKeyPress = 0;

                Debug.Log("Medium Combo");
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && time == 0)
        {
            time = keyTime;

            Debug.Log("Medium Punch");
        }

        if (Input.GetKeyDown(KeyCode.D) && time >= 0)
        {
            mediumKeyPress += 1;
            if (Input.GetKeyDown(KeyCode.D) && mediumKeyPress >= 3)
            {
                mediumKeyPress = 0;

                Debug.Log("Heavy Combo");
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && time == 0)
        {
            time = keyTime;

            Debug.Log("Heavy Punch");
        }
    }

    void BlockDamage()
    {
        if (isBlocking == true)
        {
            health -= (damage - blockEffectiveness);
        }
    }
}
