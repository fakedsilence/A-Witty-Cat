using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDie3 : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Material material;
    private bool isplayOne = false;
    private bool isplayTwo = false;
    private float fadeSpeed = 5f;
    private bool isTeleport = false;
    public Animator animator;
    private GameObject cat;
    void Start()
    {
        respawnPoint = this.transform.position;
        cat = this.gameObject;
    }

    private void Update()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        //Debug.Log(material);
        if (Trap.isDie)
        {
            this.GetComponent<Animator>().enabled = false;
            //this.GetComponent<PlayerMovement>().enabled = false;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isplayOne = true;
            Trap.isDie = false;
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
                isTeleport = false;
                this.GetComponent<Animator>().enabled = true;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
                //this.GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
}
