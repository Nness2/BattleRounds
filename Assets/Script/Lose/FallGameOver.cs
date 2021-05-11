﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallGameOver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            Destroy(collision.collider.gameObject);
            //SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
        }
        if (collision.collider.tag == "Bot")
        {
            Destroy(collision.collider.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
