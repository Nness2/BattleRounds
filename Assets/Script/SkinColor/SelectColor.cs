using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using System.Reflection;


public class SelectColor : MonoBehaviour
{
    string mobileFilePath;
    string filePath;
    string jsonString;
    private JsonData gameData;
    public GameObject characterColor;
    public int price;
    public GameObject pricePrefab;
    // Start is called before the first frame update
    void Start()
    {
        mobileFilePath = Application.persistentDataPath + "/Coins.json";
        filePath = Application.streamingAssetsPath + "/Coins.json";
        characterColor = GameObject.FindWithTag("MainSurface");


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
            }
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
        gameData = JsonMapper.ToObject(jsonString);

        price = int.Parse(gameData["ColorShop"][0]["price"][0][NameOfColor(GetComponent<Image>().color)].ToString());
        if (price > 0)
        {
            GameObject priceObj = Instantiate(pricePrefab, transform.position, transform.rotation);
            priceObj.GetComponent<Price>().price = price;
            priceObj.GetComponent<Price>().selfColor = NameOfColor(GetComponent<Image>().color);
            priceObj.GetComponent<RectTransform>().sizeDelta = transform.GetComponent<RectTransform>().sizeDelta;
            priceObj.transform.SetParent(transform, false);
            priceObj.transform.position = transform.position;
            priceObj.transform.rotation = transform.rotation;
        }
    }

    public void selctColor()
    {
        characterColor.transform.GetComponent<SkinnedMeshRenderer>().material.color = GetComponent<Image>().color;
    }

    public void SetColorAtFile()
    {
        gameData["ColorShop"][0]["currentColor"] = NameOfColor(GetComponent<Image>().color);

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
