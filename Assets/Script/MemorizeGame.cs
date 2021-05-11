using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MemorizeGame : MonoBehaviour
{
    public Image progressbarimg;
    public float totaltime = 5;

    private string jsonName;
    private string jsonString;
    private JsonData gameData;
    public GameObject countDownText;

    public List<GameObject> ResultFieldsList;
    private bool endRound;
    private int OptNbr;

    public GameObject TransitionPrefab;
    public GameObject EmojiDisplayer;
    public GameObject TextDisplayer;

    private int GpointNmb;
    private int RpointNmb;
    private int state;

    private IEnumerator EndCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        EndCoroutine = WaitAndPrint(2.0f);
        endRound = false;
        countDownText = GameObject.FindWithTag("TimerText");
        jsonName = "MemorizeGame";
        string filePath = Application.streamingAssetsPath + "/MemorizeGame.json";
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
        OptNbr = Random.Range(0, gameData[jsonName].Count);
        RpointNmb = Random.Range(0, 4);
        string emojiName = gameData[jsonName][OptNbr]["question"].ToString();
        Texture2D tex;
        //tex = Resources.Load("Images/Emoji/" + emojiName) as Texture2D;
        //EmojiDisplayer.GetComponent<Renderer>().material.mainTexture = tex;
        for (int i = 0; i < gameData[jsonName][OptNbr]["answer"].Count; i++)
        {
            emojiName = gameData[jsonName][OptNbr]["answer"][i].ToString();
            tex = Resources.Load("Images/Emoji/" + emojiName) as Texture2D;
            ResultFieldsList[i].GetComponent<Renderer>().material.mainTexture = tex;

            if (gameData["MemorizeGame"][OptNbr]["answer"][i].ToString() == gameData["MemorizeGame"][OptNbr]["answer"][int.Parse(gameData["MemorizeGame"][OptNbr]["true"][Random.Range(0, 2)].ToString())].ToString())
            {
                GpointNmb = i;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            totaltime = totaltime - Time.deltaTime;
            if (totaltime > 0)
            {
                progressbarimg.fillAmount -= 1.0f / 5 * Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < gameData[jsonName][OptNbr]["answer"].Count; i++)
                {
                    Texture2D tex = Resources.Load("Images/Emoji/interrogation") as Texture2D;
                    ResultFieldsList[i].GetComponent<Renderer>().material.mainTexture = tex;
                    if (i == GpointNmb)
                        ResultFieldsList[i].tag = "TargetPoint";
                }
                ResultFieldsList[RpointNmb].tag = "TargetPointRandom";

                state = 1;
                totaltime = 5;
                progressbarimg.fillAmount = 1;

            }

        }


        else if (state == 1)
        {
            TextDisplayer.GetComponent<TMP_Text>().text = "Find :";
            Texture2D tex = Resources.Load("Images/Emoji/" + gameData[jsonName][OptNbr]["question"].ToString()) as Texture2D;
            EmojiDisplayer.GetComponent<Renderer>().material.mainTexture = tex;
            totaltime = totaltime - Time.deltaTime;
            if (totaltime > 0)
            {
                progressbarimg.fillAmount -= 1.0f / 5 * Time.deltaTime;
            }
            else
            {
                if (endRound == false)
                {
                    for (int i = 0; i < gameData[jsonName][OptNbr]["true"].Count; i++)
                    {
                        tex = Resources.Load("Images/Emoji/" + gameData[jsonName][OptNbr]["question"].ToString()) as Texture2D;
                        ResultFieldsList[int.Parse(gameData[jsonName][OptNbr]["true"][i].ToString())].GetComponent<Renderer>().material.mainTexture = tex;

                    }
                    for (int i = 0; i < gameData[jsonName][OptNbr]["wrong"].Count; i++)
                    {
                        Destroy(ResultFieldsList[int.Parse(gameData[jsonName][OptNbr]["wrong"][i].ToString())]);
                    }
                    endRound = true;
                    StartCoroutine(EndCoroutine);

                }
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