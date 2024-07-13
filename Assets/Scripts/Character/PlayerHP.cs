using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 3;
    public static int currentHealth;
    private Vector3 respawnPoint;
    private Material material;

    public HealthBar healthBar;

    private bool isplayOne = false;
    private bool isplayTwo = false;
    private float fadeSpeed = 5f;
    private bool isTeleport = false;
    public bool isDie = false;

    private void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = this.transform.position;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        if (currentHealth == 0)
            isDie = true;
        if (isDie)
        {
            PlayerMovement.runSpeed = 0f;
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isplayOne = true;
            isDie = false;
        }
        if (isplayOne)
        {
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 0f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") < 0.01)
            {
                isplayTwo = true;
                isplayOne = false;
                isTeleport = true;
            }
        }
        if (isplayTwo)
        {
            if (isTeleport)
            {
                this.transform.position = respawnPoint;
                currentHealth = 3;
                isTeleport = false;
                this.GetComponent<Animator>().enabled = true;
                PlayerMovement.runSpeed = 7.5f;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
                //this.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        healthBar.SetHealth(currentHealth);
    }

}
