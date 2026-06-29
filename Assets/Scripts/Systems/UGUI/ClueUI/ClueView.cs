using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClueView : BasePanel
{
    public TMP_Text content;    // Start is called before the first frame update
    void Start()
    {

    }
    public void show(string cont)
    {
        content.text = cont;
    }
}
