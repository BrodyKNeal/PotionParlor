using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D body;
    public Animator animator;
    Vector2 movement;

    //Update called once per frame for physics
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; //normalize the movement vector so diagonal movement isn't faster


        //Prevents animator from viewing movement data if the player is not moving.
        //This will cause the animator to keep the player in the same direction they were facing when they stop moving
        if(movement.sqrMagnitude > 0)
        {
            animator.SetFloat("Horizontal", movement.x); //Sets the animator's variable called "Horizontal" to the player's input left/right
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude); //Sets the animator's variable called "Speed" to the magnitude of the player's movement
    }

    //Has the player actually move according to what buttons are pressed.
    void FixedUpdate()
    {
        //The math clamp prevents the player from increasing speed over time.
        float xVelocity = Mathf.Clamp((movement.x * moveSpeed * Time.fixedDeltaTime),-moveSpeed,moveSpeed); 
        float yVelocity = Mathf.Clamp((movement.y * moveSpeed * Time.fixedDeltaTime), -moveSpeed, moveSpeed);
        body.velocity = new Vector2(xVelocity, yVelocity);
        //body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
