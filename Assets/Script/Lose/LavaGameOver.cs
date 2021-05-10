using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGameOver : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
           Destroy(collision.collider.gameObject);
           Debug.Log("You Lose");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
