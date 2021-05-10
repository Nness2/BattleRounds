using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TransitionPrefab;
    public int RoundNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(TransitionPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
