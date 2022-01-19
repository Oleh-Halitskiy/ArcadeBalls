using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Magnet settings")]
    [SerializeField] private float MagnetRadius = 5;
    [SerializeField] private float MagnetForce = 5;
    void FixedUpdate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, MagnetRadius);
        int i = 0;
        while (i < hitColliders.Length - 1)
        {
            i++;
            Rigidbody2D rb = hitColliders[i].gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.AddForce((this.transform.position - hitColliders[i].transform.position) * MagnetForce * Time.smoothDeltaTime);
            }

        }
    }

}
