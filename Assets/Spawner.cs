using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnable;
    private IEnumerator coroutine;
    private float spawnInterval = 10f;
    private float minInterval = 1f;
    private float reductionInterval = 0.2f;

    void Start()
    {
        coroutine = Spawn(spawnInterval);
        StartCoroutine(coroutine);
    }

    // every 2 seconds perform the print()
    private IEnumerator Spawn(float waitTime)
    {
        while (true)
        {
            GameObject spawned = Instantiate(spawnable, new Vector3(1f, 7f, 0), Quaternion.identity);
            float speed = Random.Range(1f, 5f);
            spawned.GetComponent<AIPath>().maxSpeed = speed;
            Debug.Log("ENEMY SPAWNED WITH SPEED: " + speed);
            yield return new WaitForSeconds(spawnInterval);
            if (spawnInterval > minInterval)
            {
                spawnInterval -= reductionInterval;
            }
        }
    }

}
