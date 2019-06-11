using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public SpriteRenderer shield;
    public Vector3 shieldScale, scale;
    public float[] scaleSetUp;
    public ParticleSystem shieldExplode;
    public float startSize, endSize;


    void Start()
    {

    }

    void Update()
    {

        transform.localScale = Vector3.Lerp(Vector3.one * endSize, Vector3.one * startSize, ComboTest.blockEffectiveness / ComboTest.blockTime);

        if (ComboTest.isBlocking == true)
        {
            shield.enabled = true;
        }
        else
        {
            shield.enabled = false;
        }

        if (ComboTest.isBlocking && ComboTest.blockEffectiveness <= 0)
        {
            shieldExplode.Emit(50);
        }
    }
}
