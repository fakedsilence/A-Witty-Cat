using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject fallDetector;
    public static Vector3 respawnPoint;
    public GameObject catDie;
    public static AudioSource catDieMusic;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = this.transform.position;
        catDieMusic = catDie.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            catDieMusic.Play();
        }
    }
}
