using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanK_CamSocket : MonoBehaviour
{
    public bool InControl = false;

    [Header("Properties")]
    public bool PlayerAccesible = true;
    //public bool LerpToMe = true;

    [Header("CamProperties")]
    public Vector2 currentAxis = Vector2.zero;
    public float Zoom = 1;
            public enum CamModes{
                FreeRotation,
                AddPawnRotation,
                UsePawnRotation
            }
            public CamModes CamMode = CamModes.FreeRotation ;
    public float LerpPosition = 32;//0 Disable
    public float LerpRotation = 64;//0 Disable

}
