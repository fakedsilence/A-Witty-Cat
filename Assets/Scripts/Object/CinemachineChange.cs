using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


public class CinemachineChange : MonoBehaviour
{
    public GameObject virtualCamera1;

    private void Start()
    {
        virtualCamera1.SetActive(true);
    }
    private void Update()
    {
        if (DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            virtualCamera1.SetActive(false);
        }
        else
        {
            virtualCamera1.SetActive(true);
        }
    }
}
