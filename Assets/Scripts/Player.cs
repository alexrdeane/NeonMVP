using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region movementVariables
    public KeyCode forwardKey;
    public KeyCode backKey;
    public PlayerNum playerNum;

    private Vector3 moveDirection;
    public CharacterController characterController;
    private float jumpSpeed = 5f;
    private float speed = 1f, gravity = 20f;

    public Transform player;
    public Transform player2;
    #endregion

    #region combatVariables
    public KeyCode lightKey;
    public KeyCode mediumKey;
    public KeyCode heavyKey;

    public Attack lightAttack;
    public Attack mediumAttack;
    public Attack heavyAttack;

    public List<Combo> combos;
    public float maxComboLeeway = 0.2f;
    private float comboLeeway = 0;

    public Animator anim;
    private Attack currentAttack = null;
    private Attack prevAttack = null;
    private float attackTimer = 0;
    private float comboTimer = 0;
    //List<int> currentCombos = new List<int>();
    private int currentCombo = -1; // -1 Represents no combo
    public bool skip = false;

    public Collider[] hurtBoxes;
    #endregion

    void Start()
    {
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
        moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        //gravity is constantly being added to the player
        moveDirection.y -= notFlipped ? gravity : -gravity * Time.deltaTime;
        //character speed is based on time
        characterController.Move((transform.position.x < player2.position.x ? moveDirection : -moveDirection) * Time.deltaTime);

        //input controls for jump
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        //movement controls for player
        if (Input.GetKey(backKey))
        {
            anim.SetBool("isBack", true);
        }
        else
        {
            anim.SetBool("isBack", false);
        }

        if (Input.GetKey(forwardKey))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (attackTimer <= 0.5f)
        {
            hurtBoxes[0].enabled = true;
        }

        // If there isn't currently an attack running
        if (currentAttack == null)
        {
            Attack inputAttack = null;
            if (Input.GetKeyDown(lightKey))
                inputAttack = lightAttack;
            if (Input.GetKeyDown(mediumKey))
                inputAttack = mediumAttack;
            if (Input.GetKeyDown(heavyKey))
                inputAttack = heavyAttack;

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

        /*
        //// Loop through all combos and reset them before detecting new combos
        //List<int> remove = new List<int>();
        //for (int i = 0; i < combos.Count; i++)
        //{
        //    Combo combo = combos[i];
        //    if (combo.ContinueCombo(input))
        //    {
        //        comboLeeway = 0;
        //    }
        //    else
        //    {
        //        remove.Add(i);
        //    }
        //}
        */

        // Skips a frame after a combo to reset the combo count to stop an extra action after a combo
        if (skip)
        {
            skip = false;
            return;
        }


        /*
        ////if combo exists inputs will be used to preform the combo
        //for (int i = 0; i < combos.Count; i++)
        //{
        //    if (currentCombos.Contains(i)) continue;
        //    if (combos[i].ContinueCombo(input))
        //    {
        //        currentCombos.Add(i);
        //        comboLeeway = 0;
        //    }
        //}
        //foreach (int i in remove)
        //{
        //    combos.RemoveAt(i);
        //}
        */

        // If a combo has been detected
        if (currentCombo >= 0)
        {
            // Run the Attack (from before)
            // Attack(currentAttack);
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

    //Attack GetAttackFromType(AttackType type)
    //{
    //    if (type == AttackType.Light)
    //        return lightAttack;
    //    if (type == AttackType.Medium)
    //        return mediumAttack;
    //    if (type == AttackType.Heavy)
    //        hurtBoxes[0].enabled = false;
    //    return heavyAttack;
    //}
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
            // 
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
