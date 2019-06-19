/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCollider : MonoBehaviour
{
    public CapsuleCollider col;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            print(other.transform.tag);
            //set target to the thing in zone
            //switch state to seek
            Destroy(other.gameObject);
        }
    }

}
*/