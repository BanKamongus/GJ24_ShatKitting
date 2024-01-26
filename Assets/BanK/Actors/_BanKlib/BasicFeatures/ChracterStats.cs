using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterStats : MonoBehaviour
{
    [Header("Properties")]
    public bool Invincible = false;
    public bool KeepOnLoad = false;
    public GameObject[] SpawnOnHurt;
    public float Health_MAX = 20;
    [Header("Current")]
    public float Health = 20;

    public void ChangeHealth(float Value) {    if (Invincible || Value==0 ) { return; }
            foreach (GameObject obj in SpawnOnHurt){
                Instantiate(obj, transform.position, transform.rotation);
            }
                    Health = Mathf.Clamp(Health+Value, 0, Health_MAX);
                    if (Health <= 0) {
                        Destroy(this.gameObject);
                    }
    }

                            /////////////////////////
                            /// Handle All Collisions
                            void OnCollisionEnter(Collision collision){
                                HasDamage _HasDamage = collision.gameObject.GetComponent<HasDamage>();
                                if (_HasDamage != null) { ChangeHealth(-_HasDamage.Damage); }
                            }
                            void OnCollisionEnter2D(Collision2D collision){
                                HasDamage _HasDamage = collision.gameObject.GetComponent<HasDamage>();
                                if (_HasDamage != null) { ChangeHealth(-_HasDamage.Damage); }
                            }
    void Awake(){
        if (KeepOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }
}
