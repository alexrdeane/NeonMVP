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
    // Player1 current health
    public float curHealth = 100;
    // Player2 current health
    [Header("Reference to UI elements")]
    // Reference to Slider
    public Slider healthSlider;
    // Reference to Fill
    public Image healthFill;

    public Player currentPlayer;
    public string attackLightInput;
    public string attackMediumInput;
    public string attackHeavyInput;

    public float lightTimer = 2;
    public float mediumTimer = 2;
    public float heavyTimer = 2;
    public bool canAttack;

    private bool isLightPressed = false;
    private bool isMediumPressed = false;
    private bool isHeavyPressed = false;
    public GameObject winText;
    public GameObject player;
    
    void Update()
    {
        lightTimer += Time.deltaTime;
        mediumTimer += Time.deltaTime;
        heavyTimer += Time.deltaTime;
        healthSlider.value = Mathf.Clamp01(curHealth / maxHealth);
        ManageHealthBar();

        isLightPressed = Input.GetButtonDown(attackLightInput);
        isMediumPressed = Input.GetButtonDown(attackMediumInput);
        isHeavyPressed = Input.GetButtonDown(attackHeavyInput);

        if (lightTimer <= 0.7 && mediumTimer <= 1.1 && heavyTimer <= 1.9)
        {
            canAttack = false;
        }
        else if (lightTimer >= 0.7 && mediumTimer >= 1.1 && heavyTimer >= 1.9)
        {
            canAttack = true;
        }

    }

    void ManageHealthBar()
    {
        if (curHealth <= 0 && healthFill.enabled)
        {
            Debug.Log("Dead");
            winText.SetActive(true);
            healthFill.enabled = false;
            player.SetActive(false);
            Invoke("LoadScene", 5);
        }
        else if (!healthFill.enabled && curHealth > 0)
        {
            Debug.Log("Revive");
            healthFill.enabled = enabled;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        // What is the thing we hit
        Player otherPlayer = col.GetComponent<Player>();
        // If the thing is a Player AND not my Player
        if (otherPlayer != null && !otherPlayer.Equals(currentPlayer))
        {
            if (isLightPressed && canAttack == true)
            {
                canAttack = false;
                curHealth -= 5;
                lightTimer = 0;
            }
            if (isMediumPressed && canAttack == true)
            {
                canAttack = false;
                curHealth -= 10;
                mediumTimer = 0;
            }
            if (isHeavyPressed && canAttack == true)
            {
                canAttack = false;
                curHealth -= 13;
                heavyTimer = 0;
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}