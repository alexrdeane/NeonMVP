using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables
    public PlayerNum playerNum;
    private string horizontalButton;
    public Transform player;
    public Transform player2;
    public CharacterController characterController;
    private float speed = 1f, gravity = 20f;
    private Vector3 moveDirection;
    private Animator anim;
    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        if (playerNum == PlayerNum.Player1)
        {
            /*
            lightAttackButton = "LightAttack";
            mediumAttackButton = "MediumAttack";
            heavyAttackButton = "HeavyAttack";
            */
            horizontalButton = "Horizontal";
        }
        else if (playerNum == PlayerNum.Player2)
        {
            /*
            lightAttackButton = "LightAttack2";
            mediumAttackButton = "MediumAttack2";
            heavyAttackButton = "HeavyAttack2";
            */
            horizontalButton = "Horizontal2";
        }
    }

    void Update()
    {
        AnimatePlayerWalk();
    }

    void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement()
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
    }

    void AnimatePlayerWalk()
    {
        if(Input.GetButton(horizontalButton))
        {
            anim.SetBool("movement", true);
        }
        else
        {
            anim.SetBool("movement", false);
        }
    }

    public enum PlayerNum
    {
        Player1,
        Player2
    };
}
