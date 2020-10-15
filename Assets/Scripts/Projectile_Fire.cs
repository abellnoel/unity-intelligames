using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Fire : MonoBehaviour
{
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
