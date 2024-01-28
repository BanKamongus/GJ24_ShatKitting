using System.Collections;
using UnityEngine;

public class BGFallDown : MonoBehaviour
{
    public GameObject[] spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke the SpawnItems method every 2 seconds, starting after 0 seconds
        InvokeRepeating("SpawnItems", 0f, 2f);
    }

    void SpawnItems()
    {
        foreach (GameObject itemPrefab in spawnPrefab)
        {
            // Generate a random x position within a range (adjust the range as needed)
            float randomX = Random.Range(-5f, 5f);

            // Create a Vector3 with the random x position, and Y and Z positions of the BGFallDown object
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

            // Spawn the itemPrefab at the calculated spawnPosition
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
