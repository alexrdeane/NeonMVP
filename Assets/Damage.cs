using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//scence mannger here to restart the level when enm dead
using UnityEngine.SceneManagement;



public class Damage : MonoBehaviour
{
    public Slider oppoHealth;
    //slider for Health can be 
    public Collider hitCol;
    /// <summary>
    /// //players hit colider yo
    /// </summary>
    public Collider enmCol;
    //ennemy ditectiontions yeah

    // Start is called before the first frame update
    void Start()
    {
        //i dont know why this is here, 
    }
    /// <summary>
    /// /so like updates check the health if there is 
    ///no health, then do a thing
    //this thing atm is a debug.log and 
    //itll restart the level
    /// </summary>
    void Update()
    {
        if(oppoHealth.value <= 0)
        {
            Debug.Log("death");
            //retsart the scene can make do whatever, just a thingy to show it works
            //debug.log isnt visual lol
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider enmCol)
    {
        //make suure the right tag hits it right thingy 
        //using same way copy nd paste can be appled to various coldiers
        if (gameObject.tag == "Oppo_damage_Tag")
        {
            oppoHealth.value -= 1;
            ///do some damage, hitbar slider will do down
            Debug.Log("hiit");
        }
    }

    /////T.B////2019/////
}
/////End 