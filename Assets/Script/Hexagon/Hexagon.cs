using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    public float totaltime = 5;
    public Image progressbarimg;
    public GameObject TransitionPrefab;

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
        else
        {
            Instantiate(TransitionPrefab, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
        
    }
}
