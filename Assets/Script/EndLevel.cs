using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EndCanvas;
    public Animator animator;
    private IEnumerator EndCoroutine;

    void Start()
    {
        EndCoroutine = WaitAndPrint(2.0f);
        StartCoroutine(EndCoroutine);
    }

    // Update is called once per frame
    void EndGameSuccess()
    {

        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            GameObject.FindWithTag("CoinsCanvas").GetComponent<Coins>().AddCoinsAtFile(40);
            Instantiate(EndCanvas, Vector3.zero, Quaternion.identity);
        }

        GameObject[] botsList = GameObject.FindGameObjectsWithTag("Bot");
        if (botsList.Length > 0)
        {
            foreach (GameObject bot in botsList)
            {
                animator = bot.GetComponent<Animator>();
                animator.SetInteger("AnimState", 2);
            }
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EndGameSuccess();
    }
}
