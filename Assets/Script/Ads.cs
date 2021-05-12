using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    // Start is called before the first frame update
    string GooglePlay_ID = "4126912";
    bool GameMode = true;
    bool showed;
    private void Start()
    {
        showed = false;
        Advertisement.Initialize(GooglePlay_ID, GameMode);

    }

    private void Update()
    {
        if (!showed)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
                showed = true;
            }
        }
    }

    public void ShowAds()
    {
        Advertisement.Show();
    }
}
