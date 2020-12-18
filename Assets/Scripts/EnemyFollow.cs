using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField]private float attackDamage = 25;
    [SerializeField]private float attackSpeed = 0.5f;
    private Vector2 target;
    private bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            target = player.transform.position;

            if (target != Vector2.zero)
            {
                //move towards target
                float step = speed * Time.deltaTime;
                // move sprite towards the target location
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //destroy if not colliding with projectile or player
        GameObject other = collision.gameObject;
        if (other.tag == "Player" && canAttack)
        {
            Health healthScript = other.GetComponent<Health>();
            StartCoroutine(Attack(healthScript));
        }
    }

    IEnumerator Attack(Health health)
    {
        health.health -= attackDamage;
        yield return new WaitForSecondsRealtime(attackSpeed);
        canAttack = true;
    }
}
