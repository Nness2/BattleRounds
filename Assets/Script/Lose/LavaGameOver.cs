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
            Destroy(collision.collider.gameObject);
            //SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
        }
        if (collision.collider.tag == "Bot")
        {
            Destroy(collision.collider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
