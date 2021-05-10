using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private Rigidbody myBody;
    public float moveForce = 10f;

    private FloatingJoystick joystick;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FloatingJoystick>();

    }
    // Update is called once per frame 
    void Update()
    {
        myBody.velocity = new Vector3(joystick.Horizontal * moveForce,
            myBody.velocity.y,
            joystick.Vertical * moveForce);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
    }
}
