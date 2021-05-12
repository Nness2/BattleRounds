using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;


public class Coins : MonoBehaviour
{
    // Start is called before the first frame update

    private JsonData gameData;
    public int haveCoin;
    public Text coinText;
    string jsonString;
    string mobileFilePath;
    string filePath;
    void Start()
    {
        mobileFilePath = Application.persistentDataPath + "/Coins.json";
        filePath = Application.streamingAssetsPath + "/Coins.json";

        if (Application.platform == RuntimePlatform.Android)
        {
            jsonString = File.ReadAllText(mobileFilePath);
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }

        //JsonData itemData = JsonMapper.ToObject(jsonString);
        //jsonString = File.ReadAllText(filePath);

        gameData = JsonMapper.ToObject(jsonString);
        //Debug.Log(gameData["CalculateGame"][0]["question"]);
        haveCoin = int.Parse(gameData["Coins"].ToString());
        coinText.GetComponent<Text>().text = haveCoin.ToString();
        //AddCoinsAtFile(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoinsAtFile(int nbr)
    {
        gameData["Coins"] = nbr + haveCoin;

        if (Application.platform == RuntimePlatform.Android)
        {
            JsonWriter jw1 = new JsonWriter();
            gameData.ToJson(jw1);
            string json1 = jw1.ToString();

            File.WriteAllText(Application.persistentDataPath + "/Coins.json", json1);

            haveCoin = int.Parse(gameData["Coins"].ToString());
            coinText.GetComponent<Text>().text = gameData["Coins"].ToString();
        }
        else
        {
            JsonWriter jw1 = new JsonWriter();
            gameData.ToJson(jw1);
            string json1 = jw1.ToString();

            File.WriteAllText(filePath, json1);

            haveCoin = int.Parse(gameData["Coins"].ToString());
            coinText.GetComponent<Text>().text = gameData["Coins"].ToString();
        }
    }
}
