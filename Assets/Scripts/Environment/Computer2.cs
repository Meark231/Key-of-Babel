using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Computer2 : Singleton<Computer>
{
    public SpriteRenderer sr;
    private Material mat;

    public bool isopen = false;
    private bool canInteract = false;
    private bool isfirst = true;
    public GameObject TipsE;
    private float normalThickness = 0f;
    private float activeThickness = 0.025f;
    private void Start()
    {
        mat = sr.material;

    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F))
        {
            if (isopen == true)
            {
                PlayerState.Instance.currentps=PlayerState.ps.Movable;
                CameraFocusMove.Instance.BackToNormal();
                UIManager.Instance.ClosePanel(UIConst.Button2Panel);
                isopen = false;

            }
            else if (isopen == false&&PlayerState.Instance.currentps == PlayerState.ps.Movable)
            {
                PlayerState.Instance.currentps =PlayerState.ps.ReadingPanel;
                CameraFocusMove.Instance.FocusToPlayer();
                InputCommandSystem.Instance.lines = new List<string>
                {
                    " "
                };
                InputCommandSystem.Instance.currentLevel = 2;
                InputCommandSystem.Instance.currentWords = new List<WordType>();
                InputCommandSystem.Instance.currentInputLine = " ";
                UIManager.Instance.OpenPanel(UIConst.Button2Panel);
                isopen = true;

            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = true;

            mat.SetFloat("_OutlineThickness", activeThickness);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = false;

            mat.SetFloat("_OutlineThickness", normalThickness);
        }
    }
}
