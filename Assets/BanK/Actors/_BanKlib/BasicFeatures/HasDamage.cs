using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    public float Damage = 1;

    [Header("Dynamic")]
    public bool DynamicDamage = false;
    public Vector2 Threshold = new Vector2(4, 10);
    public float Multiplier = 0.5f;
    Rigidbody2D RB2D; bool is2D = false;
    Rigidbody RB3D;

    void Start(){
        if (DynamicDamage) {
            RB2D = GetComponent<Rigidbody2D>();
            RB3D = GetComponent<Rigidbody>();
            if (RB2D != null) { is2D = true; }
            StartCoroutine(DynamicDamageUpdate());
        }
    }
    private IEnumerator DynamicDamageUpdate(){
        while (true){
                    if (DynamicDamage)
                    {
                        if (is2D){
                            Damage = Multiplier * (Vector2.SqrMagnitude(RB2D.velocity));
                        }
                        else{
                            Damage = Multiplier * (Vector3.SqrMagnitude(RB3D.velocity));
                        }
                        if (Damage < Threshold.x) { Damage = 0;} else
                        if (Damage > Threshold.y) { Damage = Threshold.y; }
                    }
                    //else { StopCoroutine(DynamicDamageUpdate()); }

            // Wait for the next frame
            yield return null;
        }
    }



}
