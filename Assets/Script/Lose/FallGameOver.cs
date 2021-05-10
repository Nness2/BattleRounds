using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGameOver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            Destroy(collision.collider.gameObject);
            Debug.Log("You Lose");
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
