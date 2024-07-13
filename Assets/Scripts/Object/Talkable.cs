using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField]
    private bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;

    public GameObject cat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E) && DialogueManager.instance.dialogueBox.activeInHierarchy == false
            && cat.GetComponent<Animator>().GetFloat("Speed") < 0.01f && cat.GetComponent<Animator>().GetBool("IsJumping") == false)
        {
            DialogueManager.instance.ShowDialogue(lines);
        }
    }
}
