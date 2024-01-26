using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public float LifeTime = -1;
    public GameObject[] SpawnOnDeath;

    void Start(){
        if (LifeTime >= 0){    Destroy(gameObject, LifeTime);    }
    }

    void OnDestroy(){ 
            foreach (GameObject obj in SpawnOnDeath){
                Instantiate(obj, transform.position, transform.rotation);
            }
    }
}
