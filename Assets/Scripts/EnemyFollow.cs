using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Vector2 target;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindObjectOfType<Player>();
    }
}
