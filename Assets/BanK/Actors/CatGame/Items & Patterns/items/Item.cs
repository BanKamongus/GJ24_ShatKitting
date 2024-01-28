using System.Collections;
using System.Collections.Generic;
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

    public bool HasBeenUsed { get; private set; } = false;

    private bool ZLock = true;



    float G = 0.4f;
    float V = 0.16f;
    int Ylvl = 0;

    void Start()
    {
        Ylvl = Random.Range(-1, -10);
    }
    void Update()
    {
        
        if(!ZLock)
        {
            V -= G*Time.deltaTime;
            transform.position += new Vector3(0, V, 0.005f);
            transform.rotation *= Quaternion.Euler(0, 0, 4);
            if (transform.position.y < Ylvl) {
                Destroy(gameObject);
            }
        }
    }

    public void UseItem(Cat cat)
    {
        HasBeenUsed = true;
        switch (itemType)
        {
            case ItemType.Food:
                // Food interaction logic
                cat.Feed(foodPoints, foodType);
                DestroyItem(); // Destroy immediately for Food items
                break;
            case ItemType.ScoreObject:
                // Increase cat's score
                cat.IncreaseScore(scoreValue);
                ZLock = false; // Start moving and then destroy
                break;
        }
    }

    // Call this method to initiate delayed self-destruction
    public void InitiateSelfDestruction()
    {
        StartCoroutine(DelayedSelfDestruction());
    }

    private IEnumerator DelayedSelfDestruction()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        Destroy(gameObject); // Destroy the item
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}