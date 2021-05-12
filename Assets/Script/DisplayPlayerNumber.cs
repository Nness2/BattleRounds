using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerNumber : MonoBehaviour
{
    private int playerNmb;
    public Text PlyerText;
    private IEnumerator UpdateText;
    private int updatePlayer;

    // Start is called before the first frame update
    void Start()
    {
        updatePlayer = 0;
        playerNmb = Random.Range(100, 1000);
        UpdateText = WaitAndPrint(0.0001f);
        StartCoroutine(UpdateText);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (updatePlayer < playerNmb)
        {
            yield return new WaitForSeconds(waitTime);
            updatePlayer += 3;
            PlyerText.text = updatePlayer.ToString();
        }
    }
}
