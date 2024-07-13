using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie2 : MonoBehaviour
{
    public GameObject fallDetector2;

    private Vector3 respawnPoint;
    private PlayerTeleport playerTeleport;

    private void Start()
    {
        playerTeleport = GetComponent<PlayerTeleport>();
        respawnPoint = transform.position;
    }

    private void Update()
    {
        fallDetector2.transform.position = new Vector3(transform.position.x, fallDetector2.transform.position.y, fallDetector2.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector2")
        {
            transform.position = respawnPoint;
            playerTeleport.DeathZone2.SetActive(false);
            playerTeleport.bannedBackground.SetActive(true);
            playerTeleport.bannedBackground1.SetActive(true);
        }
    }
}
