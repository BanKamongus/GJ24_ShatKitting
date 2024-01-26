using UnityEngine;

public class BanK_Camera : MonoBehaviour
{
    [Header("Pivots")]
        public Transform Pivot_Current;
        public Transform MyCamera;

    [Header("Settings")]
        public Vector2 Sensitivity = new Vector2(2, 2);

    [Header("Properties")]
        public Vector2 currentAxis = Vector2.zero;
        public enum CamModes
        {
            FreeRotation,
            AddPawnRotation,
            UsePawnRotation
        }
        public CamModes CamMode = CamModes.FreeRotation;
        public float Zoom = 1.0f;
        public float LerpPosition = 32;//0 Disable
        public float LerpRotation = 64;//0 Disable









//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (!Pivot_Current) { CycleSockets(); }
    }
     
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Pivot_Current == null) { CycleSockets(); }


        UpdateCameraMatrix();
        SetZoom();
    }

//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////







    int CurrentSocket = 0;
    public void CycleSockets()
    {
        BanK_CamSocket[] AllSockets = FindObjectsOfType<BanK_CamSocket>();
        BanK_CamSocket[] AccessibleSockets = System.Array.FindAll(AllSockets, socket => socket.PlayerAccesible);
        if (AccessibleSockets.Length > 0){
            
            CurrentSocket = Mathf.Clamp(CurrentSocket, 0, AccessibleSockets.Length-1);
            AccessibleSockets[CurrentSocket].InControl = false;
            CurrentSocket = (CurrentSocket + 1) % AccessibleSockets.Length;
            Pivot_Current = AccessibleSockets[CurrentSocket].transform;
            AccessibleSockets[CurrentSocket].InControl = true;

                        // Apply Properties
                        BanK_CamSocket MySocket = AccessibleSockets[CurrentSocket];
                        Zoom = MySocket.Zoom;
                        CamMode = (CamModes)MySocket.CamMode;
                        currentAxis = MySocket.currentAxis;
                        LerpPosition = MySocket.LerpPosition;
                        LerpRotation = MySocket.LerpRotation;
        }
    }
    void HandleMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        currentAxis.x -= mouseY * Sensitivity.y;
        currentAxis.x = Mathf.Clamp(currentAxis.x, -90, 90); // Prevent Clipping
        currentAxis.y += mouseX * Sensitivity.x;
    }
    void SetZoom()
    {
        float zoomFactor = Input.GetAxis("Mouse ScrollWheel");
        Zoom = Mathf.Max(0.1f, Zoom * (1 - zoomFactor));//Rec:0.001
        MyCamera.localPosition = Vector3.Lerp( MyCamera.localPosition, new Vector3(MyCamera.localPosition.x, MyCamera.localPosition.y, -Zoom), Time.deltaTime * 16);
    }







                        void UpdateCameraMatrix()   {  LerpTransform();
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
                                    Quaternion targetRotation = Quaternion.Euler(0,0,0) ;
                                    Vector3 targetPosition = Vector3.zero ;
                                    void Update_FreeRotationMatrix(){
                                        HandleMouse();
                                        targetRotation = Quaternion.Euler(currentAxis.x, currentAxis.y, 0);
                                        targetPosition = Pivot_Current.position;
                                    }

                                    void Update_AddPawnRotationMatrix(){
                                        HandleMouse();
                                        targetRotation = Quaternion.Euler(currentAxis.x, Pivot_Current.rotation.eulerAngles.y + currentAxis.y, 0);
                                        targetPosition = Pivot_Current.position;
                                    }

                                    void Update_UsePawnRotationMatrix(){
                                        targetRotation = Pivot_Current.rotation;
                                        targetPosition = Pivot_Current.position;
                                    }
                                    void LerpTransform() {

                                        if (LerpRotation <= 0) { transform.rotation = targetRotation; } else { 
                                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * LerpRotation);
                                        }

                                        if (LerpPosition <= 0) { transform.position = targetPosition;  } else { 
                                        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * LerpPosition);
                                        }
                                    }
}
