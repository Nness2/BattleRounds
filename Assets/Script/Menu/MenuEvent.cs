using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CanvasSkin;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single); 
    }

    public void OpenSkinCanvas()
    {
        CanvasSkin.SetActive(!CanvasSkin.activeSelf);
    }
}
