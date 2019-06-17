/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRain : MonoBehaviour
{
    //The rain we want to spawn in as a list
    public GameObject[] rainToSpawn;
    //The selected fruit we want to spawn
    public int rainIndex;
    //Time that we want to spawn the rain
    public float spawnTimer;
    //The count up timer
    public float timer;

    void Start()
    {
        //Ranger with integer is not exclusive with the Max. (max not included)
        rainIndex = Random.Range(0, rainToSpawn.Length);
        //Only Range with float is inclusive with the Max. (max is included)
        spawnTimer = Random.Range(0.5f, 0.7f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
            Instantiate(rainToSpawn[rainIndex], this.transform.position, Quaternion.identity);
            timer = 0;
            //Ranger with integer is not exclusive with the Max. (max not included)
            rainIndex = Random.Range(0, rainToSpawn.Length);
            //Only Range with float is inclusive with the Max. (max is included)
            spawnTimer = Random.Range(0.5f, 0.7f);
        }
    }
}
*/