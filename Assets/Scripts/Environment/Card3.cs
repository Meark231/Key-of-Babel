using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Card3 : Singleton<Card>
{
    public SpriteRenderer sr;
    private Material mat;
    public TextAsset dialogText;
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
        if (canInteract == true && Input.GetKeyDown(KeyCode.F))
        {
            DialogSystem.Instance.sayDirect("我", "地上似乎有张卡片");
            ItemsSystem.Instance.ownitems.Add("HighCard");
            ProcessSystem.Instance.ifGetHighCard = true;
            Destroy(gameObject);
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
