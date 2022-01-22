using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private List<GameObject> Balls;
    [SerializeField] private List<GameObject> Sprites;
    [Header("Shooting settings")]
    [SerializeField] private float BallSpeed;
    [Header("Other settings")]
    [SerializeField] private GameObject NextPos1;
    [SerializeField] private GameObject NextPos2;
    private List<GameObject> sprites;
    private Camera cameraComponent;
    private Vector3 target;
    private Vector3 difference;
    private Vector2 direction;
    private float rotationZ;
    private float distance;
    GameObject ball;
    public List <GameObject> BallsToSpawn
    {
        get { return Balls; }
    }

    void Start()
    {
      cameraComponent = Camera.GetComponent<Camera>();  
    }
    private void Update()
    {
        CalculateDirection();
        Shoot(direction,rotationZ); 
    }
    private void CalculateDirection()
    {
        target = cameraComponent.ScreenToWorldPoint(Input.mousePosition);
        difference = target - transform.position;
        rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        distance = difference.magnitude;
        direction = difference / distance;
        direction.Normalize();

    }
    private void Shoot(Vector2 direction, float rotation)
    {
       if (ball == null)
        {
            ball = Instantiate(Balls[Random.Range(0, Balls.Count)]);
            ball.transform.position = transform.position;
            ball.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            ball.GetComponent<Rigidbody2D>().isKinematic = true;
            ball.GetComponent<BallController>().MgtRadius = 0;
            ball.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (Input.GetMouseButtonDown(0) && ball != null)
        {
            ball.GetComponent<Rigidbody2D>().isKinematic = false;
            ball.GetComponent<Rigidbody2D>().velocity = direction * BallSpeed;
            ball.GetComponent<BallController>().isFromPlayer = true;
            ball.GetComponent<CircleCollider2D>().enabled = true;
            ball.GetComponent<BallController>().MgtRadius = 1;
            ball = null;
        }

    }
    private void GenerateBalls()
    {
        
    }
}
