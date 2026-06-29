using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class itemview : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text name;
    public TMP_Text description;
    private PackageItem itsItem;
    public PackageTable table;
    public GameObject selected;
    void Start()
    {
        EventCenter.Instance.AddEventListener<PackageItem>("ClickItem", setSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void refresh(string wname)
    {
        itsItem = table.GetItem(wname);
        name.text = itsItem.name;

    }
    public void click()
    {
        EventCenter.Instance.EventTrigger<PackageItem>("ClickItem", itsItem);
    }
    public void setSelected(PackageItem item)
    {
        if (item == itsItem)
        {
            selected.SetActive(true);
        }
        else
        {
            selected.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (EventCenter.Instance != null)
        {
            EventCenter.Instance.RemoveEventListener<PackageItem>("ClickItem", setSelected);
        }
    }
}
