using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Fire : MonoBehaviour
{
    //TESTING FOR SWARM BEHAVIOR AND MOUSE TRACKING
    private float speed = 8.0f;
    public static Vector2 target;


    void Update()
    {     
        target = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        //move towards target
        float step = speed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
    //using to not cause physics update to collisions
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //destroy if not colliding with projectile or player
        GameObject other = collision.gameObject;
        if (other.tag != "Player" && other.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        //destroy projectile when it goes off screen
        //NOTE: Deletes when it goes off screen of ANY camera including the main window
        //      which may cause odd behavior
        Destroy(gameObject);
    }
}
