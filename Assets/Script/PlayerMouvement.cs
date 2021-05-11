using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private Rigidbody myBody;
    public float moveForce = 10f;

    private FloatingJoystick joystick;
    public Animator animator;
    public GameObject player;


    private void Start()
    {

        animator = player.GetComponent<Animator>();
    }
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FloatingJoystick>();

    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        myBody.velocity = new Vector3(Mathf.Round(joystick.Horizontal * 100f) / 100f * moveForce,
            myBody.velocity.y,
            Mathf.Round(joystick.Vertical * 100f) / 100f * moveForce);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(myBody.velocity.x, 0, myBody.velocity.z));
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);

        }
    }
}
