using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private List<GameObject> Balls;
    [Header("Shooting settings")]
    [SerializeField] private float BallSpeed;
    [Header("Other settings")]
    [SerializeField] private GameObject NextPos1;
    [SerializeField] private GameObject NextPos2;
    private List<GameObject> balls;
    private Camera cameraComponent;
    private Vector3 target;
    private Vector3 difference;
    private Vector2 direction;
    private float rotationZ;
    private float distance;
    private GameObject currentBall;
    private GameObject nextBall1;
    private GameObject nextBall2;
    public List<GameObject> BallsToSpawn
    {
        get { return Balls; }
    }

    void Start()
    {
        balls = new List<GameObject>();
        cameraComponent = Camera.GetComponent<Camera>();
    }
    private void Update()
    {
        GenerateBalls();
        CalculateDirection();
        Shoot(direction, rotationZ);
        ShowNextBall(false);
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
        if (currentBall == null)
        {
            currentBall = Instantiate(balls[0]);
            currentBall.transform.position = transform.position;
            currentBall.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            currentBall.GetComponent<Rigidbody2D>().isKinematic = true;
            currentBall.GetComponent<BallController>().MgtRadius = 0;
            currentBall.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (Input.GetMouseButtonDown(0) && currentBall != null)
        {
            currentBall.GetComponent<Rigidbody2D>().isKinematic = false;
            currentBall.GetComponent<Rigidbody2D>().velocity = direction * BallSpeed;
            currentBall.GetComponent<BallController>().isFromPlayer = true;
            currentBall.GetComponent<CircleCollider2D>().enabled = true;
            currentBall.GetComponent<BallController>().MgtRadius = 1;
            balls.RemoveAt(0);
            ShowNextBall(true);
            currentBall = null;
        }

    }
    private void GenerateBalls()
    {
        while(balls.Count < 4)
        {
            balls.Add(Balls[Random.Range(0, Balls.Count)]);
        }
    }
    private void ShowNextBall(bool needUpdate)
    {
        if (needUpdate)
        {
            Destroy(nextBall1);
            Destroy(nextBall2);
        }
        else
        {
            if (nextBall1 == null)
            {
                nextBall1 = Instantiate(balls[1]);
                nextBall1.transform.position = NextPos1.transform.position;
                nextBall1.GetComponent<Rigidbody2D>().isKinematic = true;
                nextBall1.GetComponent<BallController>().MgtRadius = 0;
                nextBall1.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (nextBall2 == null)
            {
                nextBall2 = Instantiate(balls[2]);
                nextBall2.transform.position = NextPos2.transform.position;
                nextBall2.GetComponent<Rigidbody2D>().isKinematic = true;
                nextBall2.GetComponent<BallController>().MgtRadius = 0;
                nextBall2.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}

