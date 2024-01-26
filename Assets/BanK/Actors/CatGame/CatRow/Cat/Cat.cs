using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private int currentPoints = 0;
    public int maxPoints = 100;

    private CTRL catController; // Now a private variable
    public enum ActionKey { Act01, Act02, Act03, Act04 }
    public ActionKey actionKey;


    public int CurrentScore = 0;

    [Header("UI")]
    public TextMesh TXT_Score;


    void Start()
    {
        // Automatically get the CTRL component attached to the same GameObject
        catController = GetComponent<CTRL>();

        if (catController == null)
        {
            Debug.LogError("CTRL component not found on the same GameObject");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null && IsActionKeyPressed())
        {
            Debug.Log("pressed");
            Feed(item.foodPoints);
            item.DestroyItem(); // Ensure this method is correctly named in the Item script
        }
    }

    private bool IsActionKeyPressed()
    {
        switch (actionKey)
        {
            case ActionKey.Act01:
                // Debug.Log("ACT01");
                return catController.IsAct01Triggered();
            case ActionKey.Act02:
                //Debug.Log("ACT02");
                return catController.IsAct02Triggered();
            case ActionKey.Act03:
                // Debug.Log("ACT03");
                return catController.IsAct03Triggered();
            case ActionKey.Act04:
                // Debug.Log("ACT04");
                return catController.IsAct04Triggered();
            default:
                return false;
        }
    }

    void Feed(int points)
    {
        currentPoints += points;
        if (currentPoints > maxPoints)
        {
            currentPoints = maxPoints;
        }

        Debug.Log("Cat has " + currentPoints + " points");
    }

    void Update()
    {
        TXT_Score.text = CurrentScore.ToString();
    }

}