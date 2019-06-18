using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RainHandler : MonoBehaviour
{
    public int rainPoints;
    void Start()
    {
        //destroy this rain 1.5 seconds after spawning
        Destroy(this.gameObject, 1.5f);
    }
}