using Unity.VisualScripting;
using UnityEngine;
using TMPro;

using UnityEngine.UI;
public class Itemsctrl : MonoBehaviour
{
    public itemview itemviewPrefab;
    public TMP_Text Description;
    public TMP_Text Name;
    public UnityEngine.UI.Image img;

    void Start()
    {
        foreach (string s in ItemsSystem.Instance.ownitems)
        {
            itemview view = Instantiate(itemviewPrefab, transform);
            view.refresh(s);
        }
        EventCenter.Instance.AddEventListener<PackageItem>("ClickItem", updateDescription);
    }
    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<PackageItem>("ClickItem", updateDescription);
    }

    public void updateDescription(PackageItem item)
    {
        Description.text = item.description;
        Name.text = item.name;
        img.sprite = item.img;
    }
}