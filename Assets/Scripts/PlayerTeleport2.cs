using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport2 : MonoBehaviour
{
    private GameObject currentTeleporter;
    private Material material;
    private float fadeSpeed = 5f;
    private bool isCold = true;
    private bool isplayOne = false;
    private bool isplayTwo = false;
    private bool isTeleport = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null && isCold == true)
            {
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                PlayerMovement.runSpeed = 0f;
                isplayOne = true;
                StartCoroutine(ColdTimeCo());
            }
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
                PlayerMovement.runSpeed = 7.5f;
                this.GetComponent<Animator>().enabled = true;
                this.transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                isTeleport = false;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }

    IEnumerator ColdTimeCo()
    {
        isCold = false;
        yield return new WaitForSeconds(3.0f);
        isCold = true;
    }
}
