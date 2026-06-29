using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levellog : MonoBehaviour
{
    public TextAsset text;
    // Start is called before the first frame update
    void Start()
    {
        DialogSystem.Instance.say(text);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }
}
