using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEvent : MonoBehaviour
{
    public GameObject EndCanvas;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            Destroy(collision.collider.gameObject);
            Instantiate(EndCanvas, Vector3.zero,Quaternion.identity);
            //SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
        }
        if (collision.collider.tag == "Bot")
        {
            Destroy(collision.collider.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnLobby()
    {
        SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
    }
}
