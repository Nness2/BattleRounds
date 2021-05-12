using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateColorSkin : MonoBehaviour
{
    public GameObject prefab;
    private List<Color> colorList;

    void Start()
    {
        colorList = new List<Color>();
        colorList.Add(Color.blue);
        colorList.Add(Color.green);
        colorList.Add(Color.red);
        colorList.Add(Color.white);
        colorList.Add(Color.black);
        colorList.Add(Color.gray);
        colorList.Add(Color.cyan);
        colorList.Add(Color.yellow);

        populate();
    }
    // Update is called once per frame
    void populate()
    {
        GameObject newObj;

        for (int i = 0; i < 8; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = colorList[i];
        }
    }
}
