using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    string mobileFilePath;
    string filePath;
    string jsonString;

    // Start is called before the first frame update
    void Start()
    {
        mobileFilePath = Application.persistentDataPath + "/ColorShop.json";
        filePath = Application.streamingAssetsPath + "/ColorShop.json";

        if (Application.platform == RuntimePlatform.Android)
        {
            jsonString = File.ReadAllText(mobileFilePath);
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //.GetComponent<Image>().color =
    }
}
