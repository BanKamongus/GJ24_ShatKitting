using UnityEngine;

public class BanK_CameraOLD : MonoBehaviour
{
    [Header("Pivots")]
            public Transform Pivot_Current;

    [Header("Properties")]
            public Vector2 Sensitivity = new Vector2(4, 4);
                public enum CamModes
                {
                    FreeRotation,
                    AddPawnRotation,
                    UsePawnRotation
                }

            public CamModes CamMode = CamModes.FreeRotation;
            public float ZoomSpeed = 2.0f;
            public float Zoom = 1.0f;


    void Start(){
        if (!Pivot_Current){    CycleSockets();    }
    }

    void LateUpdate(){
        HandleInput();
        UpdateCameraMatrix();

        if (Input.GetKeyDown(KeyCode.K)){    CycleSockets();   }
    }

            int CurrentSocket = 0;
            public void CycleSockets()
            {
                BanK_CamSocket[] AllSockets = FindObjectsOfType<BanK_CamSocket>();
                BanK_CamSocket[] AccessibleSockets = System.Array.FindAll(AllSockets, socket => socket.PlayerAccesible);
                if (AccessibleSockets.Length > 0)
                {
                    CurrentSocket = (CurrentSocket + 1) % AccessibleSockets.Length;
                    Pivot_Current = AccessibleSockets[CurrentSocket].transform;
                }

                            //Apply Properties
                            BanK_CamSocket MySocket = AccessibleSockets[CurrentSocket];
                            Zoom = MySocket.Zoom;
                            CamMode = (CamModes)MySocket.CamMode ;
            }


            float currentXaxis = 0.0f;
            float currentYaxis = 0.0f;
            void HandleInput()
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                Zoom -= Input.GetAxis("Mouse ScrollWheel") * (ZoomSpeed * (Zoom+0.1f) / 5);
                Zoom = Mathf.Max(Zoom, 0.002f); // Minimum zoom level Rec:0.002
                currentXaxis -= mouseY * Sensitivity.y;
                currentXaxis = Mathf.Clamp(currentXaxis, -89, 89); // Prevent Clipping
                currentYaxis += mouseX * Sensitivity.x;
            }


    void UpdateCameraMatrix()
    {
        switch (CamMode)
        {
            case CamModes.FreeRotation:
                Update_FreeRotationMatrix();
                break;
            case CamModes.AddPawnRotation:
                Update_AddPawnRotationMatrix();
                break;
            case CamModes.UsePawnRotation:
                Update_UsePawnRotationMatrix();
                break;
            default:
                Update_FreeRotationMatrix(); // Default to Free Rotation
                break;
        }
    }

    Quaternion rotation; Vector3 rotatedOffset; Vector3 desiredPosition;
    void Update_FreeRotationMatrix()
    {
        rotation = Quaternion.Euler(currentXaxis, currentYaxis, 0);
        rotatedOffset = rotation * (Zoom * Vector3.back);
        desiredPosition = Pivot_Current.position + rotatedOffset;
        transform.position = desiredPosition;
        transform.LookAt(Pivot_Current.position);
    }

    void Update_AddPawnRotationMatrix()
    {
        rotation = Quaternion.Euler(Pivot_Current.rotation.eulerAngles.x + currentXaxis, Pivot_Current.rotation.eulerAngles.y + currentYaxis, 0);

        rotatedOffset = rotation * (Zoom * Vector3.back);
        desiredPosition = Pivot_Current.position + rotatedOffset;

        transform.position = desiredPosition;
        transform.LookAt(Pivot_Current.position);
    }

    void Update_UsePawnRotationMatrix()
    {
        rotatedOffset = Pivot_Current.rotation * (Zoom * Vector3.back);
        desiredPosition = Pivot_Current.position + rotatedOffset;

        transform.position = desiredPosition;
        transform.LookAt(Pivot_Current.position);
    }

}
