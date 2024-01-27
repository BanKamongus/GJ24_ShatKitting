using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShit : MonoBehaviour
{
    public GameObject ActiveEffect;

    [Header("Team")]
    public int TeamID = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Cat GetCat = collision.gameObject.GetComponent<Cat>();
        if (GetCat != null)
        {
            if (GetCat.TeamID == TeamID) { return; }
            Instantiate(ActiveEffect,transform.position, Quaternion.identity);
            ActiveStun getActive = ActiveEffect.GetComponent<ActiveStun>();
            if (getActive != null)
            {
                getActive.Target = GetCat;
            }
            Destroy(gameObject);
        }
    }
}
