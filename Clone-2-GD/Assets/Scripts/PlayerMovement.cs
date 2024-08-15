using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    PlayerActions playerActions;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 movement; // Stores the movement input
    public GameObject fovThing;

    public override void OnStartLocalPlayer()
    {
        Instantiate(fovThing, this.transform);
    }
    void Start()
    {
        playerActions = this.GetComponent<PlayerActions>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        if(!playerActions.inVent)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }
        if(movement!=Vector2.zero)
        {
            UpdateServerMovement();
        }
    }
    [Command] 
    void UpdateServerMovement()
    {
        UpdateClientMovement();
    }

    [ClientRpc]
    void UpdateClientMovement()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void FixedUpdate()
    {
        // Move the player based on input

    }
}
