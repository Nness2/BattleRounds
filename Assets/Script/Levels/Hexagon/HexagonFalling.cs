using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonFalling : MonoBehaviour
{
    private IEnumerator selfDelete;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "player" || collision.collider.tag == "Bot")
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(selfDelete);
        }
    }

    private void Start()
    {
        selfDelete = TimeDelete(1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimeDelete(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(transform.gameObject);
    }
}
