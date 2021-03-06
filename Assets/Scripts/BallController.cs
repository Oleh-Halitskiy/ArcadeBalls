using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Magnet settings")]
    [SerializeField] private float MagnetRadius = 5;
    [SerializeField] private float MagnetForce = 5;
    [SerializeField] private GameObject Explosion;
    private List<GameObject> ballsToDestroy;
    private bool canDestroy;
    private bool test;
    public bool isFromPlayer;
    public bool canExplode;
    
    public float MgtRadius
    {
        get { return MagnetRadius; }
        set { MagnetRadius = value; }
    }
    private void Start()
    {
        canExplode = true;
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
      StackDestroy();
    }

    private void StackDestroy()
    {
        if (ballsToDestroy.Count >= 3 && canDestroy)
        {
            foreach (GameObject gameObj in ballsToDestroy)
            {
                if (gameObj != null)
                {
                    gameObj.GetComponent<BallController>().Explode();
                 
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == gameObject.tag)
            {
                collision.gameObject.transform.parent = gameObject.transform;
                ballsToDestroy.Add(collision.gameObject);
                test = true;
                if (collision.gameObject.GetComponent<BallController>().isFromPlayer == true)
                {
                    canDestroy = true;
                    return;
                }
            }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
        {
            test = false;
            ballsToDestroy.Remove(collision.gameObject);
        }
    }
    public void AddToList(GameObject ball)
    {
        ballsToDestroy.Add(ball);
    }
    private void Explode()
    {
        Destroy(gameObject, 0.3f);
    }
    private void OnDrawGizmos()
    {

        if (test)
            Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    private void OnDestroy()
    {
        if (canExplode)
        {
            EventManager.StartPointAddedEvent(10);
            // Instantiate(Explosion, transform.position, transform.rotation); // explosions here 
        }
    }
}

