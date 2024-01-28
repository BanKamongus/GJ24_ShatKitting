using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI TXT_Score;

    [Header("Movement")]
    public KeyCode moveKey; // Key to move the cat
    public float moveSpeed = 5.0f; // Speed of movement
    public float SpeedMultiplier = 1f;
    public float moveDistance = 3.0f; // Distance to move

    [Header("Poop Settings")]
    public Transform poopSpawnPoint; // Make sure this is assigned in the Inspector

    [Header("Poop Prefabs")]
    public GameObject meatPoopPrefab;
    public GameObject veggiePoopPrefab;
    public GameObject royalPoopPrefab;

    [Header("Appearance")]
    public SpriteRenderer spriteRenderer; // Assign this in the Inspector
    public Color shattingColor = Color.green; // Color during shatting
    private Color originalColor; // To store the original color

    [Header("Animation")]
    public Animator animator; // Assign this in the Inspector
    public float animationSpeed = 1.0f; // Default animation speed
    public float runningAnimationSpeed = 2.0f; // Speed when moving faster


    private Item.FoodType lastFoodType;

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

        // Store the original color of the sprite
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer not assigned!");
        }
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
            // Move to the right, but not beyond x = 10s
            if (transform.position.x < originalPosition.x + moveDistance)
            {
                Debug.Log("Move key pressed");
                MoveCat(new Vector3(moveSpeed * SpeedMultiplier * Time.deltaTime, 0, 0));
                
                // Speed up animation when moving
                if (animator != null)
                {
                    animator.speed = runningAnimationSpeed;
                }
            }
        }
        else
        {
            Debug.Log("Move key not pressed");
            // Move back to the original position
            MoveCat(new Vector3(-moveSpeed * SpeedMultiplier * Time.deltaTime, 0, 0));

            // Reset animation speed when not moving
            if (animator != null)
            {
                animator.speed = animationSpeed;
            }
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
            //Debug.Log("Collision with item detected");
            canFeed = true;
            currentItem = item;
            //StartCoroutine(DelayedDestroy(item));
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
        item.UseItem(this);
       
    }


    public void Feed(int points, Item.FoodType foodType)
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

        lastFoodType = foodType;

    }

    IEnumerator TimeToShat()
    {
        isShatting = true; // Set the flag to true as shatting starts
        ChangeSpriteColor(shattingColor);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Choose the correct poop prefab based on the last food type
        GameObject selectedPoopPrefab = null;
        switch (lastFoodType)
        {
            case Item.FoodType.Meat:
                selectedPoopPrefab = meatPoopPrefab;
                break;
            case Item.FoodType.Veggie:
                selectedPoopPrefab = veggiePoopPrefab;
                break;
            case Item.FoodType.Royal:
                selectedPoopPrefab = royalPoopPrefab;
                break;
        }

        if (selectedPoopPrefab != null)
        {
            GameObject NewOBJ = Instantiate(selectedPoopPrefab, poopSpawnPoint.transform.position, Quaternion.identity);
            BaseShit NewShit = NewOBJ.GetComponent<BaseShit>();
            NewShit.TeamID = TeamID;
        }
        else
        {
            Debug.LogError("Poop prefab for the food type not assigned!");
        }

        // Reset points to 0
        currentPoints = 0;
        ChangeSpriteColor(originalColor); // Revert color after shatting
        isShatting = false; // Reset the flag as shatting ends
    }

    private void ChangeSpriteColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }


    public void IncreaseScore(int score)
    {
        CurrentScore += score;
    }

}