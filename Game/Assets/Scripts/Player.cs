using UnityEngine;
using System.Collections;
using Lean;
using System;

public class Player : MonoBehaviour
{
    public Vector3 CurrentDirection { get; private set; }
    public Vector3 CurrentPosition { get { return _transform.position; } }

    public float _moveSpeed = 1;
    private Transform _transform;
    private Environment _environment;

    // Use this for initialization
    void Start()
    {
        CurrentDirection = Vector3.right;
        _transform = transform;
    }

    void OnEnable()
    {
        LeanTouch.OnFingerTap += OnTap;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnTap;
    }

    private void OnTap(LeanFinger finger)
    {
        if (_environment == null)
            _environment = FindObjectOfType<Environment>();

        _environment.Dig();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _transform.Translate(CurrentDirection * _moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        CurrentDirection *= -1;
    }
}