using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFlag : MonoBehaviour
{
    public bool valid = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        valid = false;
        if (collision.collider.tag == "NonWalkable")
        {
            Destroy(gameObject);
        }
    }
}
