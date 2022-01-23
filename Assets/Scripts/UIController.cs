using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    private float score;
    private string scoreString = "Score: ";
    void Start()
    {
        EventManager.PointAddedEvent += AddPoints;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = scoreString + score;
    }
    private void AddPoints(float amount)
    {
        score += amount;
    }

}
