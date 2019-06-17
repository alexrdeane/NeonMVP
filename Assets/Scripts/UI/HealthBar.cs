using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [Header("Reference to health")]
    // Player1 maximum health
    public float maxHealth = 100;
    // Player2 maximum health
    public float maxHealth2 = 100;
    // Player1 current health
    public float curHealth = 100;
    // Player2 current health
    public float curHealth2 = 100;
    [Header("Reference to UI elements")]
    // Reference to Slider
    public Slider healthSlider;
    // Reference to Fill
    public Image healthFill;
    // Reference to Slider
    public Slider healthSlider2;
    // Reference to Fill
    public Image healthFill2;

    void Update()
    {
        healthSlider.value = Mathf.Clamp01(curHealth / maxHealth);
        healthSlider2.value = Mathf.Clamp01(curHealth2 / maxHealth2);
        ManageHealthBar();
        ManageHealthBar2();
    }

    void ManageHealthBar()
    {
        if (curHealth <= 0 && healthFill.enabled)
        {
            Debug.Log("Dead");
            healthFill.enabled = false;
            //Destroy(gameObject);
            Invoke("LoadScene", 5);
        }
        else if (!healthFill.enabled && curHealth > 0)
        {
            Debug.Log("Revive");
            healthFill.enabled = enabled;
        }
    }

    void ManageHealthBar2()
    {
        if (curHealth2 <= 0 && healthFill2.enabled)
        {
            Debug.Log("Dead");
            healthFill2.enabled = false;
            //Destroy(gameObject);
            Invoke("LoadScene", 5);
        }
        else if (!healthFill2.enabled && curHealth2 > 0)
        {
            Debug.Log("Revive");
            healthFill2.enabled = enabled;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && (Input.GetAxis("LightAttack") != 0))
        {
            curHealth2 -= 10;
        }
        if (col.gameObject.tag == "Player" && (Input.GetAxis("MediumAttack") != 0))
        {
            curHealth2 -= 20;
        }
        if (col.gameObject.tag == "Player" && (Input.GetAxis("HeavyAttack") != 0))
        {
            curHealth2 -= 30;
        }



        if (col.gameObject.tag == "Player" && (Input.GetAxis("LightAttack2") != 0))
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
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}