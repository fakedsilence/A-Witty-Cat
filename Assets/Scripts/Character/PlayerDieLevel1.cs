using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieLevel1 : MonoBehaviour
{
    public static Vector3 respawnPoint;

    private void Start()
    {
        respawnPoint = transform.position;
    }
}
