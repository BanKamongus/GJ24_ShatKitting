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

    public void DestroyItem()
    {
        Destroy(gameObject);
    }



}
