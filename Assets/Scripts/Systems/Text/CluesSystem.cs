using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesSystem : Singleton<CluesSystem>
{
    public List<string> collectedClues = new List<string>();
    public string getContent(string documentId)
    {

        TextAsset asset = Resources.Load<TextAsset>("Clues/" + documentId);



        string content = asset.text;


        return content;
    }
    public void OpenClue(string documentId)
    {
        string content = getContent(documentId);



        ClueView panel = UIManager.Instance.OpenPanel(UIConst.CluePanel) as ClueView;
        panel.show(content);

    }

}
