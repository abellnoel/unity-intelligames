using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] float moveSpeed = 20f;
    Vector2 movement;
    Vector2 mousePoint;
    Vector2 targetPoint;
    float angle;

    // Update is called once per frame
    void Update()
    {
        //returns -1 left/up, 0 neutral, +1 right/down
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.velocity = movement * moveSpeed;

        //get x, y position of the mouse in the world
        mousePoint = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        targetPoint = mousePoint - rb.position;
        
        //calculate rotation
        angle = Mathf.Atan2(targetPoint.y, targetPoint.x) * Mathf.Rad2Deg - 90f; //gets angle from player to mouse point
        rb.rotation = angle;

    }

    /*
    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = movement * moveSpeed;
    }
    */
}
