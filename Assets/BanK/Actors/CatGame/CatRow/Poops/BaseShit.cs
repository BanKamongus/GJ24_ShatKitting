using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShit : MonoBehaviour
{
    public GameObject ActiveEffect;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Cat GetCat = collision.gameObject.GetComponent<Cat>();
        if (GetCat != null)
        {
            Instantiate(ActiveEffect,transform.position, Quaternion.identity);
            ActiveStun getActive = ActiveEffect.GetComponent<ActiveStun>();
            getActive.Target = GetCat;
            Destroy(gameObject);
        }
    }
}
