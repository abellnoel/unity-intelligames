using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                ConditionTool.score += 1;
            }
            Destroy(gameObject);
        }
    }
}
