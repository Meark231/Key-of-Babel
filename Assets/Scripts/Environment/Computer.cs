using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Computer : Singleton<Computer>
{
    public GameObject outlineObj;
    public TextAsset dialogText;
    public bool isopen = false;
    private bool canInteract = false;
    private bool isfirst = true;
    public GameObject TipsE;
    private void Start()
    {

        outlineObj.SetActive(false);

    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F))
        {
            if (isopen == true)
            {
                CameraFocusMove.Instance.BackToNormal();
                UIManager.Instance.ClosePanel(UIConst.Button1Panel);
                isopen = false;

            }
            else if (isopen == false)
            {
                CameraFocusMove.Instance.FocusToPlayer();
                InputCommandSystem.Instance.lines = new List<string>
                {
                    " "
                };
                InputCommandSystem.Instance.currentWords = new List<WordType>();
                InputCommandSystem.Instance.currentInputLine = " ";
                UIManager.Instance.OpenPanel(UIConst.Button1Panel);
                isopen = true;
                if (isfirst) { DialogSystem.Instance.say(dialogText); isfirst = false; }
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = true;

            if (outlineObj != null)
            {
                TipsE.SetActive(true);
                outlineObj.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = false;

            if (outlineObj != null)
            {
                TipsE.SetActive(false);
                outlineObj.SetActive(false);
            }
        }
    }
}
