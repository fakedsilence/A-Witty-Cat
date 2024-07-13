using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public Animator anim1;
    private bool isplay = true;
    public Animator anim2;
    public GameObject PlayerRef;

    private void Start()
    {

;   }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (isplay)
            {
                StartCoroutine(WaitCO());
                anim1.Play("Down");
                anim2.Play("Down1");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isplay = false;
        }
    }

    IEnumerator WaitCO()
    {
        //Debug.Log(1);
        //PlayerRef.GetComponent<PlayerMovement>().enabled = false;
        //PlayerRef.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(10f);
        //PlayerRef.GetComponent<PlayerMovement>().enabled = true;
        //Debug.Log(2);
    }
}
