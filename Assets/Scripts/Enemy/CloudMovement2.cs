using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement2 : MonoBehaviour
{
    public float speed;
    public float distance = 20f;
    private float temp;

    private void Start()
    {
        temp = distance;
    }

    private void Update()
    {
        distance -= speed * Time.deltaTime;
        if (distance >= 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (distance <= -temp)
            distance = temp;
    }
}
