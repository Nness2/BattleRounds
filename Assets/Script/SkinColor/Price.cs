using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using System.Reflection;

public class Price : MonoBehaviour
{
    string mobileFilePath;
    string filePath;
    string jsonString;
    private JsonData gameData;

    public int price;
    public string selfColor;
    public Text valueDisplay;
    void Start()
    {

        mobileFilePath = Application.persistentDataPath + "/Coins.json";
        filePath = Application.streamingAssetsPath + "/Coins.json";


        if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Coins.json")))
            {
                jsonString = File.ReadAllText(mobileFilePath);
            }
            else
            {
                UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
                www.SendWebRequest();
                while (!www.isDone)
                {
                }
                jsonString = www.downloadHandler.text;

                File.WriteAllText(Path.Combine(Application.persistentDataPath, "Coins.json"), jsonString);

            }
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
        gameData = JsonMapper.ToObject(jsonString);

        valueDisplay.text = price.ToString();
    }

    public void BuyColor()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jsonString = File.ReadAllText(mobileFilePath);
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
        gameData = JsonMapper.ToObject(jsonString);

        int haveCoin =  int.Parse(GameObject.FindWithTag("CoinValue").GetComponent<Text>().text);
        int colorPrice = int.Parse(gameData["ColorShop"][0]["price"][0][selfColor].ToString());
        if (haveCoin >= colorPrice)
        {
            gameData["ColorShop"][0]["price"][0][selfColor] = 0;
            gameData["Coins"] = haveCoin - colorPrice;
        }
        else if (haveCoin < colorPrice)
        {
            gameData["ColorShop"][0]["price"][0][selfColor] = colorPrice - haveCoin;
            gameData["Coins"] = 0;
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            JsonWriter jw1 = new JsonWriter();
            gameData.ToJson(jw1);
            string json1 = jw1.ToString();

            File.WriteAllText(mobileFilePath, json1);
        }
        else
        {
            JsonWriter jw1 = new JsonWriter();
            gameData.ToJson(jw1);
            string json1 = jw1.ToString();

            File.WriteAllText(filePath, json1);

        }

        if (haveCoin >= colorPrice)
        {
            GameObject.FindWithTag("CoinValue").GetComponent<Text>().text = (haveCoin - colorPrice).ToString();
            Destroy(transform.gameObject);
        }
        else if (haveCoin < colorPrice)
        {
            valueDisplay.text = (colorPrice - haveCoin).ToString();
            GameObject.FindWithTag("CoinValue").GetComponent<Text>().text = "0";
            gameData["ColorShop"][0]["price"][0][selfColor] = colorPrice - haveCoin;
        }

    }

    public string NameOfColor(Color c)
    {
        var props = c.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);

        foreach (var prop in props)
        {
            if ((Color)prop.GetValue(null) == c) { return prop.Name; }
        }
        return c.ToString();
    }
}
