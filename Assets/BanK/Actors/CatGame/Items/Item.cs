using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int foodPoints = 10; // Points this food provides, set this in Inspector

    void Update()
    {
        transform.position += new Vector3(-0.1f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cat GetCat = collision.gameObject.GetComponent<Cat>();
        if (GetCat != null)
        {
            GetCat.Feed(foodPoints);
            Destroy(gameObject);
        }
    }

}
