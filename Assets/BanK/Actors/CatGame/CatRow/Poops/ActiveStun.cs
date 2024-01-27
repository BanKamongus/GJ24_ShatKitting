using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStun : MonoBehaviour
{
    public float Ratio = 0.32f;
    public Cat Target;
    void Start()
    {
        Target.SpeedMultiplier = Ratio;
    }

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3) {
            Target.SpeedMultiplier = 1;
            Destroy(gameObject);
        }
    }
}
