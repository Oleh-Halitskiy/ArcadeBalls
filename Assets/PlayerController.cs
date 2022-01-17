using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Ball;
    [Header("Shooting settings")]
    [SerializeField] private float BallSpeed;
    private Camera cameraComponent;
    private Vector3 target;
    private Vector3 difference;
    private Vector3 screenToWorldPointVector;
    private Vector2 direction;
    private float rotationZ;
    private float distance;

    void Start()
    {
      screenToWorldPointVector = new Vector3();
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
        screenToWorldPointVector.x = Input.mousePosition.x;
        screenToWorldPointVector.y = Input.mousePosition.y;
        screenToWorldPointVector.z = Camera.transform.position.z;
        target = cameraComponent.ScreenToWorldPoint(screenToWorldPointVector);
        difference = target - transform.position;
        rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        distance = difference.magnitude;
        direction = difference / distance;
        direction.Normalize();

    }
    private void Shoot(Vector2 direction, float rotation)
    {
        GameObject ball = Instantiate(Ball);
        ball.transform.position = transform.position;
        ball.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        ball.GetComponent<Rigidbody2D>().velocity = direction * BallSpeed;
    }
}
