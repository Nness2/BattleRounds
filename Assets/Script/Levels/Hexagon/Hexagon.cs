using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    public float totaltime;
    public Image progressbarimg;
    public GameObject TransitionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        totaltime = 2;
    }

    // Update is called once per frame
    void Update()
    {

        totaltime = totaltime - Time.deltaTime;
        if (totaltime > 0)
        {
            progressbarimg.fillAmount -= 1.0f / 2 * Time.deltaTime;
            Debug.Log(totaltime);
        }
        else
        {
            Instantiate(TransitionPrefab, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
        
    }
}
