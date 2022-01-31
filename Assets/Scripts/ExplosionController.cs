using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 2f);
    }

}
