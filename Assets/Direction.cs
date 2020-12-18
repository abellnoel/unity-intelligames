using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    int facing = 1;
    Animator animator;
    Transform transform;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            transform = GetComponent<Transform>();
            //control direction based on where player is relative to enemy
            Vector2 scale = transform.localScale;
            animator = gameObject.GetComponent<Animator>();
            if (transform.transform.position.x < player.transform.position.x)
            {
                if (facing != 1)
                {
                    animator.transform.Rotate(0, 180, 0);
                    facing = 1;
                }
            }
            else if (transform.transform.position.x > player.transform.position.x)
            {
                if (facing != -1)
                {
                    animator.transform.Rotate(0, 180, 0);
                    facing = -1;
                }
            }
        }
    }
}
