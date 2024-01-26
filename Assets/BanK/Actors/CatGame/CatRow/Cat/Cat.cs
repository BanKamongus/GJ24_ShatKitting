using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private int currentPoints = 0;
    public int maxPoints = 100;

    private CTRL catController; // Now a private variable
    // Replace enum with KeyCode
    public KeyCode actionKey;


    public int CurrentScore = 0;

    [Header("UI")]
    public TextMesh TXT_Score;

    [Header("Movement")]
    public KeyCode moveKey; // Key to move the cat
    public float moveSpeed = 5.0f; // Speed of movement
    public float moveDistance = 2.0f; // Distance to move

    private Vector3 originalPosition; // Original position of the cat
    private Vector3 targetPosition; // Target position when moving
    private bool isMoving = false; // Flag to check if the cat is moving


    void Start()
    {
        // Automatically get the CTRL component attached to the same GameObject
        catController = GetComponent<CTRL>();

        if (catController == null)
        {
            Debug.LogError("CTRL component not found on the same GameObject");
        }

        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
    }
    void Update()
    {
        // Update score text
        TXT_Score.text = CurrentScore.ToString();

        // Handle movement input
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(moveKey))
        {
            // Move to the right
            isMoving = true;
            MoveCat(targetPosition);
        }
        else if (isMoving)
        {
            // Move back to the original position
            MoveCat(originalPosition);
            isMoving = false;
        }
    }

    private void MoveCat(Vector3 targetPos)
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
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
        return Input.GetKeyDown(actionKey);
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



}