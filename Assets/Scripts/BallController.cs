using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Magnet settings")]
    [SerializeField] private float MagnetRadius = 5;
    [SerializeField] private float MagnetForce = 5;
    private List<GameObject> ballsToDestroy;
    private GameObject tree;
    private void Start()
    {
        ballsToDestroy = new List<GameObject>();
        ballsToDestroy.Add(gameObject);
    }
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
    private void Update()
    {
       if(ballsToDestroy.Count >= 3)
        {
            foreach(GameObject gameObj in ballsToDestroy)
            {
                Destroy(gameObj,0.01f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == gameObject.tag)
        {
            ballsToDestroy.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == gameObject.tag)
        {
            ballsToDestroy.Remove(collision.gameObject);
        }
    }
}

