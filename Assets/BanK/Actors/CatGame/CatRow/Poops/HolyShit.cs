using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyShit : MonoBehaviour
{




    void OnTriggerEnter2D(Collider2D collision)
    {
        Cat GetCat = collision.gameObject.GetComponent<Cat>();
        if (GetCat != null){
            GetCat.CurrentScore -= 25;
            Destroy(gameObject);
        }
    }
}
