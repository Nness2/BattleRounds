using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameTransition : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 3.49f;
    public GameObject countDownText;
    public GameObject calculateGamePrefab;
    public GameObject SimpleGamePrefab;
    public GameObject memorizeGamePrefab;
    public GameObject LavaGamePrefab;
    public GameObject hexagonGamePrefab;


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
            int roundNubmer = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().RoundNumber;

            if (roundNubmer == 0)
            {
                Instantiate(calculateGamePrefab, transform.position, Quaternion.identity);
            }
            else if (roundNubmer == 1)
            {
                Instantiate(SimpleGamePrefab, transform.position, Quaternion.identity);
            }
            else if (roundNubmer == 2)
            {
                Instantiate(memorizeGamePrefab, transform.position, Quaternion.identity);
            }
            else if (roundNubmer == 3)
            {
                Instantiate(LavaGamePrefab, transform.position, Quaternion.identity);
            }
            else if (roundNubmer == 4)
            {
                Instantiate(hexagonGamePrefab, transform.position, Quaternion.identity);
            }
            else if (roundNubmer == 5)
            {
                SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
            }
            GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().RoundNumber++;

        }
    }

}
