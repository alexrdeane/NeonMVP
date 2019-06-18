using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
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

    private Player currentPlayer;

    private bool isPunchPressed = false;

    public bool canPunch;

    private void Start()
    {
        currentPlayer = GetComponent<Player>();
    }

    void Update()
    {
        healthSlider.value = Mathf.Clamp01(curHealth / maxHealth);
        healthSlider2.value = Mathf.Clamp01(curHealth2 / maxHealth2);
        ManageHealthBar();
        ManageHealthBar2();

        isPunchPressed = Input.GetButtonDown("LightAttack2");
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
    
    void OnTriggerStay(Collider col)
    {
        // What is the thing we hit
        Player otherPlayer = col.GetComponent<Player>();
        // If the thing is a Player AND not my Player
        if (otherPlayer != null && !otherPlayer.Equals(currentPlayer))
        {
            if (isPunchPressed && canPunch == true)
            {
                curHealth -= 10;
                canPunch = false;
            }
            else
            {
                return;
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

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}