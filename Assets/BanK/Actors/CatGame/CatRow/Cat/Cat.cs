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

    [Header("UI")]
    public TextMesh TXT_Score;

    [Header("Movement")]
    public KeyCode moveKey; // Key to move the cat
    public float moveSpeed = 5.0f; // Speed of movement
    public float moveDistance = 2.0f; // Distance to move

    [Header("Poop Settings")]
    public GameObject poopPrefab; // Assign this in the Inspector
    public Transform poopSpawnPoint; // Assign this in the Inspector

    private Vector3 originalPosition; // Original position of the cat
    private Vector3 targetPosition; // Target position when moving
    private bool isMoving = false; // Flag to check if the cat is moving

    private bool canFeed = false; // Flag to track if the cat can feed
    private Item currentItem = null; // Current item the cat can interact 


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
            Debug.Log("Action key pressed, feeding");
            Feed(currentItem.foodPoints);
            currentItem.DestroyItem();
            canFeed = false; // Reset flag
        }
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

    private bool IsActionKeyPressed()
    {
        return Input.GetKeyDown(actionKey);
    }

    void Feed(int points)
    {
        currentPoints += points;
        if (currentPoints >= maxPoints)
        {
            currentPoints = maxPoints;
            Debug.Log("Cat has reached max points. Time to shat!");

            // Start the coroutine
            StartCoroutine(TimeToShat());
        }
        else
        {
            Debug.Log("Cat has " + currentPoints + " points");
        }
    }

    IEnumerator TimeToShat()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Instantiate the poop object at the cat's current position
        if (poopPrefab != null)
        {
            Instantiate(poopPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Poop prefab not assigned!");
        }

        // Reset points to 0
        currentPoints = 0;
    }

}