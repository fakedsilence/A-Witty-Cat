using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed;
    public float distance = 10f;

    public bool direction = true;
    private float temp;

    private void Start()
    {
        temp = distance;
    }

    private void Update()
    {
        distance -= speed * Time.deltaTime;
        if(distance >= 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        if (distance <= -temp)
            distance = temp;
    }
}
