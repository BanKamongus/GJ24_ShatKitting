using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveScorePenalty : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cat GetCat = collision.gameObject.GetComponent<Cat>();
        if (GetCat != null) {
            GetCat.CurrentScore -= 25;
            Destroy(gameObject);
        }
    }
}
