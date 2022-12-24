using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igniter : MonoBehaviour
{
    //public ParticleSystem burning;
    //public ParticleSystem smoking;

    public float burnDuration = 5.0f;
    public bool startBurn = false;

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "Land")
        //{
        //    //Debug.Log("Collided with Terrain!");
        //    startBurn = true;
        //}

        if (other.gameObject.tag == "InitiaIgnite")
        {
            Physics.IgnoreCollision(other.collider, other.collider);
        }
    }

    private void Update()
    {
        if (startBurn)
        {
            //Debug.Log("Start Burning!");
            startBurning();
        }
    }

    private void startBurning()
    {
        Destroy(this.gameObject, burnDuration);
    }

    IEnumerator WaitFewSeconds(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}
