using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
{
    Vector2 movementInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
    
    
    movementInput.Normalize();

    rb.velocity = movementInput * playerSpeed;

    if (movementInput.x != 0 || movementInput.y != 0)
    {
        animator.SetFloat("X", movementInput.x);
        animator.SetFloat("Y", movementInput.y);
        animator.SetBool("IsWalking", true);
    }
    else
    {
        animator.SetBool("IsWalking", false);
    }
}
    private void Update()
    {
        if (Time.timeScale==0f){
            movementJoystick.ResetInput();
            
        }
        
    }



    }

