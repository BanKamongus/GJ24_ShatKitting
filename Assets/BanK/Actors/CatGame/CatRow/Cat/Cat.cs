using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private int currentPoints = 0;
    public int maxPoints = 100;

    // Replace enum with KeyCode
    public KeyCode actionKey;


    public int CurrentScore = 0;

    [Header("Team")]
    public int TeamID = 1;


    [Header("UI")]
    public TextMesh TXT_Score;

    [Header("Movement")]
    public KeyCode moveKey; // Key to move the cat
    public float moveSpeed = 5.0f; // Speed of movement
    public float SpeedMultiplier = 1f;
    public float moveDistance = 3.0f; // Distance to move

    [Header("Poop Settings")]
    public GameObject poopPrefab; // Assign this in the Inspector
    public Transform poopSpawnPoint; // Assign this in the Inspector

    private Vector3 originalPosition; // Original position of the cat
    private Vector3 targetPosition; // Target position when moving
    private bool isMoving = false; // Flag to check if the cat is moving

    private bool canFeed = false; // Flag to track if the cat can feed
    private Item currentItem = null; // Current item the cat can interact 
    private bool isShatting = false; // Flag to track if the cat is shatting


    void Start()
    {

        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
    }
    void Update()
    {
        // Update score text
        TXT_Score.text = CurrentScore.ToString();

        // Handle movement input
        HandleMovement();

        // Check for action key press and feed if possible
        if (canFeed && Input.GetKeyDown(actionKey) && currentItem != null)
        {
            HandleItemInteraction(currentItem);
            canFeed = false; // Reset flag after interaction
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(moveKey))
        {
            // Move to the right, but not beyond x = 10
            if (transform.position.x < originalPosition.x + 10)
            {
                MoveCat(new Vector3(moveSpeed * SpeedMultiplier * Time.deltaTime, 0, 0));
            }
        }
        else
        {
            // Move back to the original position
            MoveCat(new Vector3(-moveSpeed * SpeedMultiplier * Time.deltaTime, 0, 0));
        }
    }

    private void MoveCat(Vector3 movement)
    {
        // Move towards the target direction
        transform.position += movement;

        // Ensure the cat doesn't move past its original position when moving back
        if (transform.position.x < originalPosition.x)
        {
            transform.position = new Vector3(originalPosition.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log("Collision with item detected");
            canFeed = true;
            currentItem = item;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            canFeed = false;
            currentItem = null;
        }
    }

    private void HandleItemInteraction(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Food:
                if (!isShatting)
                {
                    Feed(item.foodPoints); // Feed food points
                }
                break;
            case Item.ItemType.ScoreObject:
                IncreaseScore(item.scoreValue); // Increase score
                break;
        }
        item.DestroyItem();
    }

    private bool IsActionKeyPressed()
    {
        return Input.GetKeyDown(actionKey);
    }

    public void Feed(int points)
    {
        if (!isShatting) // Check if the cat is not in the shat process
        {
            currentPoints += points;
            if (currentPoints >= maxPoints)
            {
                currentPoints = maxPoints;
                Debug.Log("Cat has reached max points. Time to shat!");

                if (!isShatting) // Check again to prevent multiple coroutine starts
                {
                    StartCoroutine(TimeToShat());
                }
            }
            else
            {
                Debug.Log("Cat has " + currentPoints + " points");
            }
        }
    }

    IEnumerator TimeToShat()
    {
        isShatting = true; // Set the flag to true as shatting starts

        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Instantiate the poop object at the cat's current position
        if (poopPrefab != null)
        {
            GameObject NewOBJ = Instantiate(poopPrefab, poopSpawnPoint.transform.position, Quaternion.identity);
            BaseShit NewShit = NewOBJ.GetComponent<BaseShit>();
            NewShit.TeamID = TeamID;
        }
        else
        {
            Debug.LogError("Poop prefab not assigned!");
        }

        // Reset points to 0
        currentPoints = 0;

        isShatting = false; // Reset the flag as shatting ends
    }

    public void IncreaseScore(int score)
    {
        CurrentScore += score;
    }

}