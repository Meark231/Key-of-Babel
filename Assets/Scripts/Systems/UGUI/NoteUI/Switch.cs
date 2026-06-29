using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject TagPanel;
    public GameObject ItemPanel;
    public GameObject Tagtag;
    public GameObject TagtagOn;
    public GameObject TagColl;
    public GameObject TagCollOn;
    public bool isTag = true;
    public void switchToItem()
    {
Tagtag.SetActive(true);
        TagtagOn.SetActive(false);
        TagColl.SetActive(false);
        TagCollOn.SetActive(true);


        
        ItemPanel.SetActive(true);
        TagPanel.SetActive(false);
    }
    public void switchToTag()
    {
        Tagtag.SetActive(false);
        TagtagOn.SetActive(true);
        TagColl.SetActive(true);
        TagCollOn.SetActive(false);
        ItemPanel.SetActive(false);
        TagPanel.SetActive(true);
    }
}
