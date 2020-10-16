using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Fire : MonoBehaviour
{
    //TESTING FOR FOLLOW BEHAVIOR AND MOUSE TRACKING
    private float speed = 10.0f;
    private float followDistance = 0.5f;
    private float damage = 10;
    private Vector2 target;
    public GameObject leader;

    void Update()
    {   
        if (leader == null)
        {
            //initial leader will follow mouse (followers too if their leader dies)
            target = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        }
        else
        {
            target = leader.transform.position;
        }
        //move towards target, slows down if too close
        float stepSpeed = speed;
        if (Vector2.Distance(target, transform.position) < followDistance) {
            stepSpeed /= 2;
        }
        float step = stepSpeed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
    //using to not cause physics update to collisions
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //subtract health from on collision
        GameObject other = collision.gameObject;
        if (other.tag != "Player" && other.tag != "Projectile")
        {
            Health healthScript = other.GetComponent<Health>();
            healthScript.health -= damage;
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
