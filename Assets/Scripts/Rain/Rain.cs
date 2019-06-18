using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    //The rain we want to spawn in as a list
    public GameObject[] rainToSpawn;
    //The selected fruit we want to spawn
    public int rainIndex;
    //Time that we want to spawn the rain
    public float spawnTimer;
    //The count up timer
    public float timer;

    void Awake()
    {
        Destroy(gameObject, 0.1f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
            Instantiate(rainToSpawn[rainIndex], this.transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}