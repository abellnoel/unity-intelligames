using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianWander : MonoBehaviour
{ 
    public GameObject targetPrefab;
    bool targetReached = false;
    bool wanderStarted = false;
    private float wanderRange = 5f;
    private float speed = 3.5f;
    float moveToX;
    float moveToY;
    private int facing = 1;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        //establish target
        if (!wanderStarted)
        { 
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;
            float randX = Random.Range(wanderRange * -1, wanderRange);
            float randY = Random.Range(wanderRange * -1, wanderRange);

            Vector3 wanderSpot = new Vector3(x + randX, y + randY, 0);

            //create wanderSpot target
            GameObject target = Instantiate(targetPrefab, wanderSpot, Quaternion.identity);
                //set move coordinates to target
                if (target != null)
                {
                    moveToX = target.transform.position.x;
                    moveToY = target.transform.position.y;
                    Destroy(target);
                    //Debug.Log("TARGET FOUND");
                    wanderStarted = true;
                }
        }
        //move towards target
        else
        {
            //move towards target
            float step = speed * Time.deltaTime;
            Vector2 targetPoint = new Vector2(moveToX, moveToY);
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, step);

            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(moveToX, moveToY, 0)); 
            if (distance <= 1)
            {
                //Debug.Log("TARGET REACHED");
                wanderStarted = false;
            }
        }

        //control direction based on where player is relative to enemy
        Vector2 scale = transform.localScale;
        Animator animator = gameObject.GetComponent<Animator>();
        if (transform.transform.position.x < moveToX)
        {
            if (facing != 1)
            {
                animator.transform.Rotate(0, 180, 0);
                facing = 1;
            }
        }
        else if (transform.transform.position.x > moveToX)
        {
            if (facing != -1)
            {
                animator.transform.Rotate(0, 180, 0);
                facing = -1;
            }
        }
      }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("TARGET DELETED");
        wanderStarted = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("TARGET DELETED");
        wanderStarted = false;
    }
}
