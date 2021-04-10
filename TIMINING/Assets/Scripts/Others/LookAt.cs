using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LookAt : MonoBehaviour
{
    public TextMeshPro _text;
    public float _speed = 90f;
    public bool _idle = true;
    public bool _onHover = false;
    private Vector3 startPos;
    public float height;
    public float speed;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        //Look at the camera
        _text.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
         Camera.main.transform.rotation * Vector3.up);
        //Idle animation.
        if (_idle)
        {
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * _speed);
        }
        //On Hover animation
        if (_onHover)
        {
            transform.position = startPos + Vector3.up * height * Mathf.Sin(Time.time * speed);
        }
    }
}