/*
* File:			Scoring.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au) & Ruben Antao (s200493@students.aie.edu.au)
* Edit Dates:
*	First:		17/06/2021
*	Last:		18/06/2021
* Summary:
*	Static scoring system used throughout each level to calculate and manage the players score.
*/

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
    [HideInInspector] public int deadEnemies = 0;

    [HideInInspector] public bool Won { 
        get => (deadEnemies >= enemies);
    }

    void Start()
    {
        main = this;

        maxScore += enemies * baseEnemyValue;

        //portalValue = (int)(maxScore * (portalPercentValue));
        maxScore += portalValue;
    }

    public void AddPoints()
    {
        deadEnemies += 1;
        playerScore += baseEnemyValue;
    }

    public void AddThrow()
    {
        throws += 1;
    }

    public int Goal()
    {
        playerScore += portalValue;
        playerScore *= 1 - ((throws - 1) / 100);

        CalculatePercentage();

        return playerScore;
    }

    void CalculatePercentage()
    {
        playerPercentage = playerScore / maxScore;
    }
}