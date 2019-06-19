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

    public Animator anim;

    private Attack currentAttack = null;
    public float attackTimer = 0f;
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
                inputAttack = heavyAttack;
            }

            if (inputAttack != null)
            {
                Attack(inputAttack);
                attackTimer = 0f;

            }

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

        // Skips a frame after a combo to reset the combo count to stop an extra action after a combo
        if (skip)
        {
            skip = false;
            return;
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
}

[System.Serializable]
public class Attack
{
    //name of animation and time of animation so that  the animator can find it automatically
    public float length;
    public AttackType type;
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
