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
