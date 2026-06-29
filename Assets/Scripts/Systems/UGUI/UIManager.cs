using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, string> pathDict;
    private Dictionary<string, GameObject> prefabDict;
    public Dictionary<string, BasePanel> panelDict;
    private Transform _uiRoot;
    private UIManager()
    {
        InitDicts();
    }
    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = GameObject.Find("Canvas").transform;
            }
            return _uiRoot;
        }
    }
    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        pathDict = new Dictionary<string, string>()
        {
            {UIConst.DialogPanel,"Prefabs/UGUI/DialogPanel"},
            {UIConst.Button1Panel,"Prefabs/UGUI/Button1Panel"},
            {UIConst.Button2Panel,"Prefabs/UGUI/Button2Panel"},
            {UIConst.Button3Panel,"Prefabs/UGUI/Button3Panel"},
            {UIConst.NotePanel,"Prefabs/UGUI/NotePanel"},
            {UIConst.CluePanel,"Prefabs/UGUI/CluePanel"}
        };
    }
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/UGUI/" + name;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(name, panelPrefab);
        }
        GameObject panelObj = Instantiate(panelPrefab, UIRoot);
        panel = panelObj.GetComponent<BasePanel>();
        panel.OpenPanel(name);
        panelDict.Add(name, panel);
        return panel;
    }
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        panelDict.TryGetValue(name, out panel);
        panel.ClosePanel();
        return true;
    }
}


public class UIConst
{
    public const string DialogPanel = "DialogPanel";
    public const string Button1Panel = "Button1Panel";
    public const string Button2Panel = "Button2Panel";
    public const string Button3Panel = "Button3Panel";
    public const string NotePanel = "NotePanel";
    public const string CluePanel = "CluePanel";
}