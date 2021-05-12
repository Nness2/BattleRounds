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
    // Start is called before the first frame update
    void Start()
    {
        mobileFilePath = Application.persistentDataPath + "/Coins.json";
        filePath = Application.streamingAssetsPath + "/Coins.json";
        characterColor = GameObject.FindWithTag("MainSurface");


        if (Application.platform == RuntimePlatform.Android)
        {
            jsonString = File.ReadAllText(mobileFilePath);
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
        gameData = JsonMapper.ToObject(jsonString);

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

            File.WriteAllText(Application.persistentDataPath + "/Coins.json", json1);
            
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
