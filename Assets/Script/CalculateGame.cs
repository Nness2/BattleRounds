using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CalculateGame : MonoBehaviour
{
    public Image progressbarimg;
    public float totaltime = 5;

    private string jsonString;
    private JsonData gameData;
    public GameObject countDownText;

    public List<GameObject> ResultFieldsList;
    private bool endRound;
    private int OptNbr;

    public GameObject TransitionPrefab;

    private IEnumerator EndCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        EndCoroutine = WaitAndPrint(1.0f);
        endRound = false;
        countDownText = GameObject.FindWithTag("TimerText");

        string filePath = Application.streamingAssetsPath + "/CalculateGame.json";
        string jsonString;

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            www.SendWebRequest();
            while (!www.isDone)
            {
            }
            jsonString = www.downloadHandler.text;

        }
        else
        {
            jsonString = File.ReadAllText(filePath);

        }

        JsonData itemData = JsonMapper.ToObject(jsonString);
        //jsonString = File.ReadAllText(filePath);

        gameData = JsonMapper.ToObject(jsonString);
        //Debug.Log(gameData["CalculateGame"][0]["question"]);
        OptNbr = Random.Range(0, gameData["CalculateGame"].Count); ;

        countDownText.GetComponent<TMP_Text>().text = gameData["CalculateGame"][OptNbr]["question"].ToString();

        for (int i = 0; i < gameData["CalculateGame"][OptNbr]["answer"].Count; i++)
        {
            ResultFieldsList[i].GetComponent<TMP_Text>().text = gameData["CalculateGame"][OptNbr]["answer"][i].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        totaltime = totaltime - Time.deltaTime;
        if (totaltime > 0)
        {
            progressbarimg.fillAmount -= 1.0f / 5 * Time.deltaTime;
        }
        else
        {
            if (endRound == false)
            {
                for (int i = 0; i < gameData["CalculateGame"][OptNbr]["answer"].Count; i++)
                {
                    //Debug.Log(int.Parse(gameData["CalculateGame"][OptNbr]["true"].ToString()));
                    //Debug.Log(int.Parse(gameData["CalculateGame"][0]["true"].ToString()));

                    if (int.Parse(ResultFieldsList[i].GetComponent<TMP_Text>().text) != int.Parse(gameData["CalculateGame"][OptNbr]["true"].ToString()))
                        Destroy(ResultFieldsList[i].transform.parent.gameObject);
                }
                endRound = true;
                StartCoroutine(EndCoroutine);
            }

        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(TransitionPrefab, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
    }
}