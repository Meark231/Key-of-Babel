using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected bool isRemove=false;
    protected new string name;//这里的new是为了隐藏父类的name属性，避免混淆
    public virtual void OpenPanel(string name)
    {
        this.name=name;
        gameObject.SetActive(true);
    }
    public virtual void ClosePanel()
    {
        isRemove=true;
        gameObject.SetActive(false);
        Destroy(gameObject);
        if (UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
    }
}
