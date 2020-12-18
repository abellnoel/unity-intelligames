using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Text score;
    public Text finalScore;
    public Text health;
    public Text civilians;
    public GameObject player;
    public static float healthNum;

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + ConditionTool.score;
        finalScore.text = "Score\n" + ConditionTool.score;

        if (player != null) {
            healthNum = player.GetComponent<Health>().health;
            health.text = "Health: " + player.GetComponent<Health>().health;
        }
        GameObject[] civs = GameObject.FindGameObjectsWithTag("Civilian");
        civilians.text = "Civilians: " + civs.GetLength(0);
    }
}
