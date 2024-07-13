using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManagerPalace1 : MonoBehaviour
{
    public Animator transitionAnim;

    public GameObject dialogueBox;

    public TextMeshProUGUI dialogueText, nameText;

    [TextArea(1, 8)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    [SerializeField] private float textSpeed;

    private bool isScrolling = false;  //DEFAULT VALUE IS FALSE

    public GameObject police;
    public GameObject minister;

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
        currentLine = 1;
    }
    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (isScrolling == false)
                {
                    Debug.Log(currentLine);
                    if (currentLine >= 11)
                    {
                        minister.SetActive(true);
                        police.SetActive(false);
                    }
                    else
                    {
                        police.SetActive(true);
                        minister.SetActive(false);
                    }
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        //dialogueText.text = dialogueLines[currentLine];  //LINE BY LINE
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);  //BOX HIDE
                        StartCoroutine(LoadSceneCO());
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] _newLines)
    {
        dialogueLines = _newLines;
        currentLine = 0;

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];  //LINE BY LINE

        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);
        //Player.GetComponent<Animator>().enabled = false;
    }

    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()
    {
        Debug.Log(1);
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;  //SHOW EACH WORD
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }

    IEnumerator LoadSceneCO()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
