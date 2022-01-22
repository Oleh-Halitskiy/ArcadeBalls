using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private List<GameObject> balls;
    private Vector3 spawnVector;

    void Start()
    {
        balls = Player.GetComponent<PlayerController>().BallsToSpawn;
        spawnVector = new Vector3(-2.3f, 4.495f, 0);
        CreateLevel();
    }
    void CreateLevel()
    {
        for(int i = 1; i < 20; i++)
        {
          GameObject g = Instantiate(balls[Random.Range(0, balls.Count)]);
          g.transform.position = spawnVector;
          spawnVector.x += 1;
            if(i % 5 == 0)
            {
                spawnVector.x = Random.Range(-1.6f, -2.3f);
                spawnVector.y += -1; 
            }
        }
    }
}
