using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Image progressbarimg;
    public float totaltime = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totaltime = totaltime - Time.deltaTime;
        if (totaltime > 0)
        {
            progressbarimg.fillAmount -= 1.0f / 5 * Time.deltaTime;
        }
    }
}
