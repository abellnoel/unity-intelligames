using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionTool : MonoBehaviour
{
    public GameObject deathUI;
    public static int score = 0;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //all civlians die END
        GameObject civlian = GameObject.FindGameObjectWithTag("Civilian");
        if (civlian == null)
        {
            Destroy(player);
            Debug.Log("ALL CIVLIANS DEAD");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        //player die END
        if (player == null)
        {
            deathUI.SetActive(true);
            Debug.Log("PLAYER DEAD");
            Debug.Log("SCORE: " + score);
        }
    }
}
