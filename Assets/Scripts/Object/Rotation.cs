using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed;
    public bool ClockwizeRotation;

    private void Update()
    {
        if(ClockwizeRotation == false)
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
        else
        {
            rotZ -= Time.deltaTime * RotationSpeed; 
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
