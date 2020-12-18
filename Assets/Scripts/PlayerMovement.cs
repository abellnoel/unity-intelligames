using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] Animator animator;
    [SerializeField] Transform transform = null;

    Vector2 movement;
    Vector2 mousePoint;
    int facing = 1;


    // Update is called once per frame
    void Update()
    {
        //returns -1 left/up, 0 neutral, +1 right/down
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //control run/idle animation
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("velocity", 1);
        }
        else
        {
            animator.SetFloat("velocity", 0);
        }
        //control direction
        Vector2 scale = transform.localScale;
        if (movement.x > 0)
        {
            if (facing != 1)
            {
                animator.transform.Rotate(0, 180, 0);
                facing = 1;
            }
        }
        else if (movement.x < 0)
        {
            if (facing != -1)
            {
                animator.transform.Rotate(0, 180, 0);
                facing = -1;
            }
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
