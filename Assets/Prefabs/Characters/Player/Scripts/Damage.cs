/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//scence mannger here to restart the level when enm dead
using UnityEngine.SceneManagement;



public class Damage : MonoBehaviour
{
    public Slider Health;
    //slider for Health can be 
    public Collider hitCol;
    /// <summary>
    /// //players hit colider yo
    /// </summary>

    /// <summary>
    /// /so like updates check the health if there is 
    ///no health, then do a thing
    //this thing atm is a debug.log and 
    //itll restart the level
    /// </summary>
    void Update()
    {
        if(Health.value <= 0)
        {
            Debug.Log("death");
            //retsart the scene can make do whatever, just a thingy to show it works
            //debug.log isnt visual lol
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider hitCol)
    {
        //make suure the right tag hits it right thingy 
        //using same way copy nd paste can be appled to various coldiers
        if (gameObject.tag == "punchCol")
        {
            Health.value -= 3;
            ///do some damage, hitbar slider will do down
            Debug.Log("hiit");
        }
        if (gameObject.tag == "kickCol")
        {
            Health.value -= 5;
            ///do some damage, hitbar slider will do down
            Debug.Log("hiit");
        }
        if (gameObject.tag == "kickCol2")
        {
            Health.value -= 10;
            ///do some damage, hitbar slider will do down
            Debug.Log("hiit");
        }
    }

    /////T.B////2019/////
}
/////End 
*/