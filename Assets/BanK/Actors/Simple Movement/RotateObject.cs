using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Adjust the torque in the Inspector
    public float torque = 50f;

    private Rigidbody rb;

    private void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        // Rotate the object around the Y-axis using Rigidbody.AddTorque
        rb.AddTorque(Vector3.up * torque);

    }
}
