using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BotIA : MonoBehaviour
{
    private GameObject[] TargetPoints;
    private GameObject[] TargetPointsRand;
    private GameObject[] array;

    private Vector3 pointTargeted;
    public float moveForce = 10f;
    public int state;
    public Animator animator;
    Vector3 dir;
    int OptNbr;
    //1->Good, 2->Random
    public int IAMode;
    public float Timing;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        Timing = UnityEngine.Random.Range(0.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Timing = Timing - Time.deltaTime;

        if (state == 0)
        {
            if (IAMode == 1)
            {
                TargetPoints = GameObject.FindGameObjectsWithTag("TargetPoint");
                TargetPointsRand = GameObject.FindGameObjectsWithTag("TargetPointRandom");
            }
            else if (IAMode == 2)
            {
                TargetPointsRand = GameObject.FindGameObjectsWithTag("TargetPointRandom");
                TargetPoints = GameObject.FindGameObjectsWithTag("TargetPoint");
                TargetPoints = TargetPointsRand.Concat(TargetPoints).ToArray();
            }

            if (TargetPoints.Length > 0)
            {
                OptNbr = UnityEngine.Random.Range(0, TargetPoints.Length + TargetPointsRand.Length-2);
                pointTargeted = TargetPoints[OptNbr].transform.position;
                dir = (pointTargeted - transform.position).normalized * moveForce;
                state = 1;
            }
        }

        if ((state == 1 ||state == 2 ) && Timing < 0)
        {
            dir = (pointTargeted - transform.position).normalized * moveForce;
            GetComponent<Rigidbody>().velocity = new Vector3(dir.x, GetComponent<Rigidbody>().velocity.y,dir.z);
        }

        if (Vector3.Distance(pointTargeted, transform.position) < 1 )
        {
            state = 2;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (TargetPoints.Length > 0)
        {
            if (TargetPoints[0] == null && state == 2)
            {
                state = 0;
            }
        }
        if (TargetPoints.Length == 0)
        {
            state = 0;
            Timing = UnityEngine.Random.Range(0.5f, 2f);
        }


        if (GetComponent<Rigidbody>().velocity.x != 0f || GetComponent<Rigidbody>().velocity.z != 0f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z));

            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);

        }
    }
}
