// Ruben

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Scoring : MonoBehaviour
{
    public static Scoring main;

    public int maxScore = 0;
    public int enemies = 0;
    public float portalPercentValue = 0.1f;
    public int baseEnemyValue = 10;

    public int portalValue = 100;
    [HideInInspector] public int playerScore = 0;
    [HideInInspector] public int playerPercentage;
    [HideInInspector] public int throws = 0;

    void Start()
    {
        main = this;

        maxScore += enemies * baseEnemyValue;

        //portalValue = (int)(maxScore * (portalPercentValue));
        maxScore += portalValue;
    }

    public void AddPoints()
    {
        playerScore += baseEnemyValue;
    }

    public void AddThrow()
    {
        throws += 1;
    }

    public void Goal()
    {
        playerScore += portalValue;
        playerScore /= throws;

        CalculatePercentage();
    }

    void CalculatePercentage()
    {
        playerPercentage = playerScore / maxScore;
    }
}