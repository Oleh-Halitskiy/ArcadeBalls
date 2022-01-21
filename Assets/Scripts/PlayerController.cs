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
    private Camera cameraComponent;
    private Vector3 target;
    private Vector3 difference;
    private Vector2 direction;
    private float rotationZ;
    private float distance;
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
        if(Input.GetMouseButtonDown(0))
        {
            Shoot(direction,rotationZ);
        }
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
        GameObject ball = Instantiate(Balls[Random.Range(0,3)]);
        ball.transform.position = transform.position;
        ball.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        ball.GetComponent<Rigidbody2D>().velocity = direction * BallSpeed;
    }
}
