using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogSystem : Singleton<DialogSystem>
{
    private TextAsset dialogFile;
    private int dialogIndex = 0;
    private string[] Rows;
    public bool ifReading = false;
    private DialogPanel currentPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ifReading)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ShowDialogRow();
            }
        }
    }
    public void ReadText(TextAsset _textAsset)
    {
        Rows = _textAsset.text.Split('\n');

    }
    public void say(TextAsset _dialogFile)
    {
        ifReading = true;
        ReadText(_dialogFile);
        if (CameraFocusMove.Instance != null) CameraFocusMove.Instance.FocusToPlayer();
        dialogIndex = 0;
        BasePanel basePanel = UIManager.Instance.OpenPanel(UIConst.DialogPanel);
        currentPanel = basePanel as DialogPanel;
        ShowDialogRow();
    }
    public void sayDirect(string name, string content)
    {
        ifReading = true;
        if (CameraFocusMove.Instance != null) CameraFocusMove.Instance.FocusToPlayer();
        //哈哈 这里巧妙利用index还停留在上次的end 所以直接一句话就能简单用
        BasePanel basePanel = UIManager.Instance.OpenPanel(UIConst.DialogPanel);
        currentPanel = basePanel as DialogPanel;
        currentPanel.UpdateText(name, content);
    }
    public void ShowDialogRow()
    {
        foreach (var row in Rows)
        {
            string[] cells = row.Split(',');
            if (cells[0] == "c" && int.Parse(cells[1]) == dialogIndex)
            {
                currentPanel.UpdateText(cells[2], cells[3]);
                dialogIndex = int.Parse(cells[4]);
                break;
            }
            else if (cells[0] == "END")
            {
                ifReading = false;
                UIManager.Instance.ClosePanel(UIConst.DialogPanel);
                if (CameraFocusMove.Instance != null) { if (Computer.Instance.isopen == false) CameraFocusMove.Instance.BackToNormal(); }
                break;
            }
        }
    }
}
