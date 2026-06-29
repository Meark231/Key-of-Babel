using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Meark/PackageTable", fileName = "PackageTable")]
public class PackageTable : ScriptableObject
{
    public List<PackageItem> DataList = new List<PackageItem>();
    public PackageItem GetItem(string name)
    {
        foreach (PackageItem item in DataList)
        {
            if (item.name == name)
            {
                return item;
            }

        }
        return null;
    }
}
[System.Serializable]
public class PackageItem
{
    public string name;
    public string description;
    public string type;
    public Sprite img;
}
