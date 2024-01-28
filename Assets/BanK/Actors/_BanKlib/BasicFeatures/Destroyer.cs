using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float LifeTime = -1;
    public GameObject[] SpawnOnDeath;
    public bool instantiateOnDeath = true; // Flag to control instantiation on death

    void Start()
    {
        if (LifeTime >= 0)
        {
            Destroy(gameObject, LifeTime);
        }
    }

    void OnDestroy()
    {
        if (instantiateOnDeath)
        {
            foreach (GameObject obj in SpawnOnDeath)
            {
                Instantiate(obj, transform.position, transform.rotation);
            }
        }
        // No need to call Destroy(this) here, as the script will be destroyed along with the GameObject
    }

    public void DestroyTarget(GameObject Target)
    {
        if (instantiateOnDeath)
        {
            foreach (GameObject obj in SpawnOnDeath)
            {
                Instantiate(obj, Target.transform.position, Target.transform.rotation);
            }
        }
        Destroy(Target);
    }
}
