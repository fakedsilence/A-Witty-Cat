using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance = 25f;

    public bool movingRight = true;
    private float fovangle;

    public Transform groundDetection;
    private void Start()
    {
        fovangle = GetComponent<FOV>().angle;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        distance -= speed * Time.deltaTime;
        if(distance <= -50f || distance >= 50f)
        {
            if (movingRight == true)
            {
                Debug.Log("1");
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                distance = 0f;
                fovangle = 360 - fovangle;
            }
            else
            {
                Debug.Log("2");
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                distance = 0f;
                fovangle = 360 - fovangle;
            }
        }    
    }
}
