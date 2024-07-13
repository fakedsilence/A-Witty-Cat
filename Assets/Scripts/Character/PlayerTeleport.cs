using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporterC;
    private GameObject currentTeleporterA;
    private Material material;
    private float fadeSpeed = 5f;
    private bool isCold = true;
    private bool isplayOne = false;
    private bool isplayTwo = false;
    public bool isTeleportA = false;
    public bool isTeleportC = false;
    private bool isplayA = false;
    private bool isplayC = false;

    public GameObject DeathZone2;
    public GameObject DeathZone1;
    public GameObject bannedBackground;
    public GameObject bannedBackground1;

    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;

    // Start is called before the first frame update
    void Start()
    {
        currentTeleporterA = null;
        currentTeleporterC = null;
        DeathZone2.SetActive(false);
        DeathZone1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporterA != null && isCold == true)
            {
                DeathZone1.SetActive(true);
                PlayerMovement.runSpeed = 0f;
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isplayOne = true;
                StartCoroutine(ColdTimeCo());
                isplayA = true;
            }
        }
        if (isplayOne && isplayA)
        {
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 0f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") < 0.01)
            {
                isplayTwo = true;
                isplayOne = false;
                isTeleportA = true;
            }
        }
        if(isplayTwo && isplayA)
        {
            bannedBackground1.SetActive(false);
            if (isTeleportA)
            {
                PlayerMovement.runSpeed = 7.5f;
                this.GetComponent<Animator>().enabled = true;
                this.transform.position = currentTeleporterA.GetComponent<Teleporter>().GetDestination().position;
                isTeleportA = false;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if(material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
                isplayA = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporterC != null && isCold == true)
            {
                cloud1.GetComponent<CloudMovement>().enabled = true;
                cloud2.GetComponent<CloudMovement>().enabled = true;
                cloud3.GetComponent<CloudMovement>().enabled = true;
                DeathZone2.SetActive(true);
                PlayerMovement.runSpeed = 0f;
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isplayOne = true;
                StartCoroutine(ColdTimeCo());
                isplayC = true;
            }
        }
        if (isplayOne && isplayC)
        {
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 0f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") < 0.01)
            {
                isplayTwo = true;
                isplayOne = false;
                isTeleportC = true;
            }
        }
        if (isplayTwo && isplayC)
        {
            bannedBackground.SetActive(false);
            if (isTeleportC)
            {
                PlayerMovement.runSpeed = 7.5f;
                this.GetComponent<Animator>().enabled = true;
                this.transform.position = currentTeleporterC.GetComponent<Teleporter>().GetDestination().position;
                isTeleportC = false;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
                isplayC = false;
                this.GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("TeleporterA"))
        {
            currentTeleporterA = collision.gameObject;
        }

        if (collision.CompareTag("TeleporterC"))
        {
            Debug.Log(3);
            currentTeleporterC = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("TeleporterA"))
        {
            if(collision.gameObject == currentTeleporterA)
            {
                currentTeleporterA = null;
            }
        }
        if (collision.CompareTag("TeleporterC"))
        {
            if (collision.gameObject == currentTeleporterC)
            {
                currentTeleporterC = null;
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
