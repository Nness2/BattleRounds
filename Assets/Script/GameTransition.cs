using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTransition : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 3.49f;
    public GameObject countDownText;
    public GameObject calculateGamePrefab;

    //public GameObject StartWall;
    //private GameObject Wall;

    void Start()
    {
        //countDownText = GameObject.FindGameObjectWithTag("TimerText");//.GetComponent<TMP_Text>();
        countDownText = GameObject.FindWithTag("TimerText");
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countDownText.GetComponent<TMP_Text>().text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            Destroy(gameObject);
            Instantiate(calculateGamePrefab, transform.position, Quaternion.identity);
            //Destroy(Wall);

        }
    }

}
