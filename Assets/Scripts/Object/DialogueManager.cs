using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;  //MAKER SINGLETON PATTERN

    public GameObject dialogueBox;  //DISPLAY OR HIDE
    public TextMeshProUGUI dialogueText;
    public GameObject Player;

    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private bool isScrolling = false;  //DEFAULT VALUE IS FALSE
    [SerializeField] private float textSpeed;

    public GameObject catdialogueBox;
    public static bool isFinished = false;
    public TextMeshProUGUI catdialogueText;

    public Animator transitionAnim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(isScrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        //dialogueText.text = dialogueLines[currentLine];  //LINE BY LINE
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        catdialogueBox.SetActive(true);
                        dialogueBox.SetActive(false);  //BOX HIDE
                    }
                }
            }
        }
        if(catdialogueBox.activeInHierarchy)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log(2);
                PlayerMovement.runSpeed = 7.5f;
                catdialogueBox.SetActive(false);
                StartCoroutine(LoadSceneCO());
                //Player.GetComponent<Animator>().enabled = true;//MAKER CAN MOVE NOW
                isFinished = true;
            }
        }
    }

    public void ShowDialogue(string[] _newLines)
    {
        dialogueLines = _newLines;
        currentLine = 0;

        //dialogueText.text = dialogueLines[currentLine];  //LINE BY LINE

        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);

        PlayerMovement.runSpeed = 0f; //MAKER CANNOT MOVE NOW
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Player.GetComponent<Animator>().enabled = false;
    }

    private IEnumerator ScrollingText()
    {
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
        Debug.Log(3);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        Debug.Log(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
