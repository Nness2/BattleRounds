using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaGameOver : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
