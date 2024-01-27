using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(-3.0f * Time.deltaTime, 0, 0);
    }
}
