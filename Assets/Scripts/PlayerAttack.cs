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

    //function to spawn projectile, projectile movement is controlled in it's script
    IEnumerator Attack(int fireStream)
    {
        GameObject previousProjectile = null;
        for (int i = 0; i < fireStream; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                                                origin.position,
                                                origin.rotation);
            previousProjectile = projectile;
            yield return new WaitForSecondsRealtime(fireInterval);
        }
        yield return null;
    }
}
