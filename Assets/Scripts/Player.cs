using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    #region movementVariables
    public PlayerNum playerNum;
    private string horizontalButton;
    public Transform player;
    public Transform player2;
    public CharacterController characterController;

    private float speed = 1f, gravity = 20f;
    private Vector3 moveDirection;
    #endregion

    #region combatVariables
    private string lightAttackButton;
    private string mediumAttackButton;
    private string heavyAttackButton;
    public Attack lightAttack;
    public Attack mediumAttack;
    public Attack heavyAttack;

    public List<Combo> combos;
    public Animator anim;
    public Collider[] hurtBoxes;

    private float maxComboLeeway = 0.2f;
    private float comboLeeway = 0;
    private Attack currentAttack = null;
    private Attack prevAttack = null;
    private float attackTimer = 0;
    private float comboTimer = 0;
    private int currentCombo = -1; // -1 Represents no combo
    private bool skip = false;
    #endregion

    void Start()
    {

        if (playerNum == PlayerNum.Player1)
        {
            lightAttackButton = "LightAttack";
            mediumAttackButton = "MediumAttack";
            heavyAttackButton = "HeavyAttack";
            horizontalButton = "Horizontal";
        }
        else if (playerNum == PlayerNum.Player2)
        {
            lightAttackButton = "LightAttack2";
            mediumAttackButton = "MediumAttack2";
            heavyAttackButton = "HeavyAttack2";
            horizontalButton = "Horizontal2";
        }

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        PrimeCombos();
    }

    void PrimeCombos()
    {
        // Loop through all combos
        foreach (var combo in combos)
        {
            combo.onInputted.AddListener(() =>
            {
                skip = true;
                Combo(combo);
                ResetCombos();
            });
        }
    }

    void Update()
    {
        bool notFlipped = transform.position.x < player2.position.x;
        transform.rotation = notFlipped ? Quaternion.Euler(0f, 90f, 0f) : Quaternion.Euler(0f, 270f, 0f);
        //when the key assigned for vertical movement in the input manager is pushed times speed and moveDirection together
        moveDirection = new Vector3(0, 0, Input.GetAxis(horizontalButton));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        //gravity is constantly being added to the player
        moveDirection.y -= gravity * Time.deltaTime;
        //character speed is based on time
        characterController.Move(moveDirection * Time.deltaTime);

        //movement controls for player
        if (playerNum == PlayerNum.Player1)
        {
            if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                anim.SetBool("isBack", true);
                anim.SetBool("isWalking", false);
            }
            else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isBack", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isBack", false);
            }
        }
        else if (playerNum == PlayerNum.Player2)
        {
            if (Input.GetButton("Horizontal2") && Input.GetAxisRaw("Horizontal2") < 0)
            {
                anim.SetBool("isBack", true);
                anim.SetBool("isWalking", false);
            }
            else if (Input.GetButton("Horizontal2") && Input.GetAxisRaw("Horizontal2") > 0)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isBack", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isBack", false);
            }
        }

        if (attackTimer >= 1.25f)
        {
            hurtBoxes[0].enabled = true;
        }

        // If there isn't currently an attack running
        if (currentAttack == null)
        {
            Attack inputAttack = null;
            if (Input.GetButtonDown(lightAttackButton))
            {
                inputAttack = lightAttack;
            }
            if (Input.GetButtonDown(mediumAttackButton))
            {
                inputAttack = mediumAttack;
            }
            if (Input.GetButtonDown(heavyAttackButton))
            {
                hurtBoxes[0].enabled = false;
                inputAttack = heavyAttack;
            }

            if (inputAttack != null)
            {
                Attack(inputAttack);
                attackTimer = 0f;

                // Loop through all created combos
                foreach (var combo in combos)
                {
                    // If input attack maches the combo's required attack
                    if (combo.ContinueCombo(currentAttack))
                    {
                        // Reset timer
                        comboLeeway = 0;
                    }
                }
            }
            return;
        }
        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= currentAttack.length)
            {
                currentAttack = null;
                attackTimer = 0f;
            }
        }

        prevAttack = currentAttack;

        // If a current combo has been detected
        if (currentCombo >= 0)
        {
            // Increase combo leeway timer
            comboLeeway += Time.deltaTime;
            // If combo leeway reaches max combo leeway
            if (comboLeeway >= maxComboLeeway)
            {
                // Reset the combos
                ResetCombos();
            }
        }
        else
        {
            comboLeeway = 0;
        }

        // Skips a frame after a combo to reset the combo count to stop an extra action after a combo
        if (skip)
        {
            skip = false;
            return;
        }
    }

    //after a combo is pressed it removes it from active
    void ResetCombos()
    {
        //resets the leeway timer when a combo is attempted
        comboLeeway = 0;
        attackTimer = 0;

        // Loop through all the combos
        foreach (var combo in combos)
        {
            // Reset each combo
            combo.ResetCombo();
        }
    }

    // Plays the attack
    void Attack(Attack attack)
    {
        currentAttack = attack;
        anim.SetTrigger("Attack");
        // Convert enum to int (type-casting)
        anim.SetInteger("AttackType", (int)attack.type);
    }

    void Combo(Combo combo)
    {
        comboTimer = combo.length;
        anim.SetTrigger("Combo");
        // Convert enum to int (type-casting)
        anim.SetInteger("ComboType", 0);
    }
}

[System.Serializable]
public class Attack
{
    //name of animation and time of animation so that  the animator can find it automatically
    public float length;
    public AttackType type;
}

[System.Serializable]
public class Combo
{
    public string name;
    public float length = 0.7f;
    public List<Attack> attacks;
    public UnityEvent onInputted;
    private int currentAttackIndex = 0;

    //continue combo if correctly inputted
    public bool ContinueCombo(Attack currentAttack)
    {
        // If Combo's required attack matches the current input attack
        if (attacks[currentAttackIndex].type == currentAttack.type)
        {
            currentAttackIndex++;
            if (currentAttackIndex >= attacks.Count)
            {
                onInputted.Invoke();
                currentAttackIndex = 0;
            }
            return true;
        }
        else
        {
            currentAttackIndex = 0;
            return false;
        }
    }
    public AttackType CurrentComboInput()
    {
        if (currentAttackIndex >= attacks.Count) return AttackType.Error;
        return attacks[currentAttackIndex].type;
    }

    //resets input of current input after a combo is completed
    public void ResetCombo()
    {
        currentAttackIndex = 0;
    }
}

//enum of attack types
public enum AttackType
{
    Error = -1,
    Light = 0,
    Medium = 1,
    Heavy = 2
};

public enum PlayerNum
{
    Player1,
    Player2
};
