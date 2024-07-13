using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public GameObject fallDetector1;

    private Vector3 respawnPoint;
    private PlayerTeleport playerTeleport;

    private void Start()
    {
        playerTeleport = GetComponent<PlayerTeleport>();
        respawnPoint = transform.position;  
    }

    private void Update()
    {
        fallDetector1.transform.position = new Vector3(transform.position.x, fallDetector1.transform.position.y, fallDetector1.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector1")
        {
            transform.position = respawnPoint;
            playerTeleport.DeathZone1.SetActive(false);
            playerTeleport.bannedBackground1.SetActive(true);
        }
    }
}
