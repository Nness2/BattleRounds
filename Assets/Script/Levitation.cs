using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{
    public float speedRotation = 1f;
    public float speedBounce = 1f;
    public float bounceIntensity = 1f;

    private Transform _transform;
    private Vector3 _initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();

        _initialPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Rotate(0, speedRotation * Time.deltaTime, 0);

        _transform.position = new Vector3(_transform.position.x ,(float) (_initialPosition.y + Math.Sin(Time.time * speedBounce) * bounceIntensity), _transform.position.z);
    }
}
