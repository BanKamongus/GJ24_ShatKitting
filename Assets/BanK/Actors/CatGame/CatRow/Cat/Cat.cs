using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public int CurrentScore = 0;
    private int currentPoints = 0; 
    public int maxPoints = 100; 

    // Function to add points
    public void Feed(int points)
    {
        currentPoints += points;
        if (currentPoints > maxPoints)
        {
            currentPoints = maxPoints;
        }

        Debug.Log("Current Points: " + currentPoints);
    }
}