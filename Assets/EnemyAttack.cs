using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float attackSpeed = 1f;
    private bool canAttack = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player" && canAttack)
        {
            Health healthScript = other.GetComponent<Health>();
            canAttack = false;
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
