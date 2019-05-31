using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//This script requires the component Character controller to be attached to the Game Objects
[RequireComponent(typeof(CharacterController))]
//Character Movement
//this script will require a charactercontroller 
//to reference later
public class player1 : MonoBehaviour
{
    #region Variables
    [Header("PLAYER MOVEMENT")]
    [Space(5)]
    [Header("Characters MoveDirection")]
    //vector3 called moveDirection
    public Vector3 moveDirection;
    //we will use this to apply movement in worldspace
    //private CharacterController (https://docs.unity3d.com/ScriptReference/CharacterController.html) charC
    public CharacterController tbcharacterController;
    [Header("Character Variables")]
    //public float variables jumpSpeed, speed, gravity
    public float jumpSpeed = 10;
    public float speed = 1f, gravity = 20;
    public static bool canMove = true;
    #endregion
    #region Start
    private void Start()
    {
        tbcharacterController = GetComponent<CharacterController>();
        //characterController is attached to player
        canMove = true;

    }
    //charc is on this game object we need to get the character controller that is attached to it
    #endregion
    #region Update
    private void Update()
    {
        //if our character is grounded
        if (tbcharacterController.isGrounded)
        {
            //we are able to move in game scene meaning
            //Input Manager(https://docs.unity3d.com/Manual/class-InputManager.html)
            //Input(https://docs.unity3d.com/ScriptReference/Input.html)
            //moveDir is equal to a new vector3 that is affected by Input.Get Axis.. Horizontal, 0, Vertical
            moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
            //moveDir is transformed in the direction of our moveDir
            moveDirection = transform.TransformDirection(moveDirection);
            //our moveDir is then multiplied by our speed
            moveDirection *= speed;
            //we can also jump if we are grounded so
            //in the input button for jump is pressed then
            if (Input.GetButton("Jump"))
            {
                //our moveDir.y is equal to our jump speed
                moveDirection.y = jumpSpeed;
            }
            //add sprint speed
            //     if (Input.GetButton("Sprint"))
            //    {
            ///       moveDirection *= speed * 1.5f;


            //    }

            //add crouch speed
            //  if (Input.GetButton("Crouch"))
            //   {
            //        moveDirection *= 0.1f * speed;

            //    }
            //  }

            //regardless of if we are grounded or not the players moveDir.y is always affected by gravity timesed my time.deltaTime to normalize it
           
        }
        moveDirection.y -= gravity * Time.deltaTime;
        //we then tell the character Controller that it is moving in a direction timesed Time.deltaTime
        tbcharacterController.Move(moveDirection * Time.deltaTime);
    }
    #endregion
}










