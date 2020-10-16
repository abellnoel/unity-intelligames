using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]Transform origin;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float chargeMax;
    [SerializeField] private float chargeRate;
    [SerializeField] private float fireInterval;
    [SerializeField] private float lifetime;
    private float charge;
    private int fireStreamNum;

    // Update is called once per frame
    void Update()
    {
        //while button is held
        if (Input.GetMouseButton(0))
        {
            if (charge < chargeMax)
            {
                charge += chargeRate * Time.deltaTime;
                Debug.Log("charge: " + charge);
            }
            else //
            {
                charge = chargeMax;
            }
        }
        //when button is released
        if (Input.GetMouseButtonUp(0))
        {
            fireStreamNum = Mathf.FloorToInt(charge); //make charge int
                                                      //using floor so it doesn't go above max
            Debug.Log("fireStreamNum: " + fireStreamNum);
            StartCoroutine(Attack(fireStreamNum));
            charge = 0; //reset charge
        }
    }

    //function to spawn projectile, projectile movement is controlled in it's script, delete all after time
    IEnumerator Attack(int fireStream)
    {
        GameObject previousProjectile = null;
        GameObject[] projectileArray = new GameObject[fireStream]; //list of all chained projectiles
        //spawn objects
        for (int i = 0; i < fireStream; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                                                    origin.position,
                                                    origin.rotation);
            projectileArray[i] = projectile;
            Projectile_Fire script = projectile.GetComponent<Projectile_Fire>();
            if (previousProjectile != null) //first projectile keeps default value
            {
                script.leader = previousProjectile;
            }
            previousProjectile = projectile;
            yield return new WaitForSecondsRealtime(fireInterval);
        }
        yield return new WaitForSecondsRealtime(lifetime);
        //destory objects if they exist after lifetime seconds
        foreach (GameObject g in projectileArray)
        {
            Object.Destroy(g);
            yield return new WaitForSecondsRealtime(fireInterval); //in same order they spawned
        }
    }
}
