using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Scoring : MonoBehaviour
{
    public static Scoring main;

    public int maxScore = 0;
    public float portalPercentValue = 0.1f;
    public int portalValue;
    public int baseEnemyValue = 10;
    public int playerScore = 0;
    public int playerPercentage;
    void Start()
    {
        main = this;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject Enemy in Enemies)
        {
            maxScore += baseEnemyValue;
            //maxScore += Enemy.GetComponent<NPC>().value;
        }
        portalValue = (int)(maxScore * (portalPercentValue));
        maxScore += portalValue;
    }
    public void AddPoints()
    {
        playerScore += baseEnemyValue;
    }
    public void Goal()
    {
        playerScore += portalValue;
        CalculatePercentage();
    }
    void CalculatePercentage()
    {
        playerPercentage = playerScore / maxScore;
    }
}
