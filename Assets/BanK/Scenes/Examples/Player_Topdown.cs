using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Topdown : MonoBehaviour
{
    public BanK_CamSocket MyCamSocket;
    public float moveSpeed = 5f;  // Adjust this value to set the movement speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!MyCamSocket.InControl) { return; }
        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Move the Rigidbody
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.AddForce( new Vector2(direction.x * moveSpeed, direction.y * moveSpeed));
    }
}

