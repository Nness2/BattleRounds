using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FloorIsLava : MonoBehaviour
{
    public Image progressbarimg;
    public float totaltime;

    private string jsonString;
    private JsonData gameData;
    public GameObject countDownText;
    
    private bool endRound;
    private int OptNbr;

    public GameObject TransitionPrefab;
    public GameObject CirclePrefab;
    public GameObject LavaPrefab;


    private IEnumerator EndCoroutine;
    private string jsonName;
    public Transform HightPoint;
    public Transform groundPosition;
    private List<GameObject> prefabObjects;
    private int state;


    void Start()
    {
        totaltime = 3;
        state = 0;
        prefabObjects = new List<GameObject>();
        EndCoroutine = WaitAndPrint(3.0f);
        endRound = false;
        countDownText = GameObject.FindWithTag("TimerText");

        string filePath = Application.streamingAssetsPath + "/FloorIsLava.json";
        string jsonString;
        jsonName = "FloorIsLava";
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
        //Debug.Log(jsonString);
        JsonData itemData = JsonMapper.ToObject(jsonString);
        //jsonString = File.ReadAllText(filePath);

        gameData = JsonMapper.ToObject(jsonString);
        //Debug.Log(gameData["CalculateGame"][0]["question"]);
        OptNbr = Random.Range(0, gameData[jsonName].Count);
        //Debug.Log(gameData[jsonName][OptNbr]["level"][0]["pos"]["x"]);

        for (int i = 0; i < gameData[jsonName][OptNbr]["level"].Count; i++)
        {
            GameObject prefab = Instantiate(CirclePrefab, 
                new Vector3(int.Parse(gameData[jsonName][OptNbr]["level"][i]["pos"]["x"].ToString()),
                HightPoint.localPosition.y,
                int.Parse(gameData[jsonName][OptNbr]["level"][i]["pos"]["y"].ToString())),
                Quaternion.identity);
            prefabObjects.Add(prefab);
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
                progressbarimg.fillAmount -= 1.0f / 3 * Time.deltaTime;
            }
            else
            {
                GameObject prefab = Instantiate(LavaPrefab, groundPosition.position, Quaternion.identity);
                prefabObjects.Add(prefab);
                
                state = 1;
                totaltime = 1;
                //progressbarimg.fillAmount = 1;

            }
        }
        else if (state == 1)
        {
            totaltime = totaltime - Time.deltaTime;
            if (totaltime > 0)
            {
                //progressbarimg.fillAmount -= 1.0f / 5 * Time.deltaTime;
            }
            else
            {
                if (endRound == false)
                {
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
        foreach (GameObject obj in prefabObjects)
        {
            Destroy(obj);
        }
    }
}
