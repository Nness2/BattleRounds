using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using System.Reflection;

public class LoadColor : MonoBehaviour
{
    string mobileFilePath;
    string filePath;
    string jsonString;
    private JsonData gameData;

    // Start is called before the first frame update
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

        gameData = JsonMapper.ToObject(jsonString);
        //Debug.Log(gameData["CalculateGame"][0]["question"]);
        //haveCoin = int.Parse(gameData["currentColor"].ToString());
        transform.GetComponent<SkinnedMeshRenderer>().material.color = ToColor(gameData["ColorShop"][0]["currentColor"].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //.GetComponent<Image>().color =
    }

    public Color ToColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
    }


}
