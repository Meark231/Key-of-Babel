using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ezmanager : MonoBehaviour
{
    public TextAsset text;
    // Start is called before the first frame update
    void Start()
    {
        DialogSystem.Instance.say(text);
    }


}
