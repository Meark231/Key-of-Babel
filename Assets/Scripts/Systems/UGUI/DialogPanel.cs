using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
public class DialogPanel : BasePanel
{



    // Start is called before the first frame update

    public UnityEngine.UI.Image sprite;

    public TMP_Text nameText;
    public TMP_Text contentText;


    public List<Sprite> sprites = new List<Sprite>();
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    private void Awake()
    {
        imageDic["我"] = sprites[0];
        imageDic["桌子"] = sprites[1];
        imageDic["控制台"]=sprites[2];
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        contentText.text = _text;
        if (typeCoroutine != null) StopCoroutine(typeCoroutine);
        typeCoroutine = StartCoroutine(TypeText(contentText, _text, 0.06f));

        sprite.sprite = imageDic[_name];


    }
    private Coroutine typeCoroutine;
    IEnumerator TypeText(TMP_Text tMP_Text, string str, float interval)
    {
        int i = 0;
        while (i <= str.Length)
        {
            tMP_Text.text = str.Substring(0, i++);
            yield return new WaitForSeconds(interval);
        }
    }



}


