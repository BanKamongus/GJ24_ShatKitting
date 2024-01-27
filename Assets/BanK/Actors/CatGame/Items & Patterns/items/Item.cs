using UnityEngine;

public class Item : MonoBehaviour
{
    // Enum to distinguish between item types


    public enum ItemType
    {
        Food,
        ScoreObject
    }

    // Enum for different types of food
    public enum FoodType
    {
        Meat,
        Veggie,
        Royal
    }

    [Header("ObjectType")]
    public ItemType itemType;
    public int scoreValue = 5; // Score value for score object, set this in Inspector

    [Header("FoodType")]
    public FoodType foodType;
    public int foodPoints = 10; // Points this food provides, set this in Inspector

    // Add any additional properties for different effects of food types

    void Update()
    {
        // Item logic (if any) goes here
    }

    public void UseItem(Cat cat)
    {
        switch (itemType)
        {
            case ItemType.Food:
                // Different effects based on food type
                switch (foodType)
                {
                    case FoodType.Meat:
                        // Apply effect for Food Type 1
                        cat.Feed(foodPoints);
                        break;
                    case FoodType.Veggie:
                        // Apply effect for Food Type 2
                        cat.Feed(foodPoints);
                        break;
                    case FoodType.Royal:
                        // Apply effect for Food Type 3
                        cat.Feed(foodPoints);
                        break;
                }
                break;
            case ItemType.ScoreObject:
                // Increase cat's score
                cat.IncreaseScore(scoreValue);
                break;
        }

        DestroyItem();
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
