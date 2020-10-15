using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform origin;
    public GameObject projectilePrefab;

    public float projectileSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    //function to shoot projectile
    void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, 
                                            origin.position, 
                                            origin.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(origin.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
