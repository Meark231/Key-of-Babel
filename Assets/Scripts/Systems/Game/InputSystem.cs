using System.Collections.Generic;
using UnityEngine;

public class InputCommandSystem : Singleton<InputCommandSystem>
{
    public List<WordType> currentWords = new List<WordType>();

    public int maxCommandLength = 4;
    public string content = "";
    public List<string> lines = new List<string>();
    void Start()
    {

    }

    public string currentInputLine = "";
    private void RefreshContent()
    {
        content = "";

        for (int i = 0; i < lines.Count; i++)
        {
            content += lines[i] + "\n";
        }


    }
    public void InputWord(WordType word)
    {

        // 如果按的是“输入”
        if (word == WordType.Enter)
        {


            currentInputLine = "";

            ExecuteCommand();
            lines.Add(" ");
            RefreshContent(); return;
        }

        currentWords.Add(word);

        currentInputLine = GetCurrentCommand();

        lines[lines.Count - 1] = currentInputLine;
        RefreshContent();
        // 防止乱按太多
        if (currentWords.Count > maxCommandLength - 1)
        {
            Debug.Log("输入太长，自动清空");
            ClearCommand();
        }
    }

    private void ExecuteCommand()
    {
        if (currentWords.Count == 4)
        {
            lines.Add("无效指令");
            ClearCommand();
            return;
        }
        if (currentWords.Count <= 2)
        {
            if ((currentWords[0] == WordType.Open || currentWords[0] == WordType.Close) && (currentWords[1] == WordType.Light || currentWords[1] == WordType.Power || currentWords[1] == WordType.Door))
            {
                lines.Add("无指定范围的权限");
                ClearCommand();
                return;
            }
            lines.Add("无效指令");
            ClearCommand();
            return;
        }


        WordType action = currentWords[0];
        WordType range = currentWords[1];
        WordType target = currentWords[2];



        if (action == WordType.Open && target == WordType.Light)
        {

            OpenLocalLight();
            if (ProcessSystem.Instance.IfPowerful == true) lines.Add("执行成功");
            else lines.Add("电力不足");
        }
        else if (action == WordType.Close && target == WordType.Light)
        {

            CloseLocalLight();
            lines.Add("执行成功");
        }
        else if (action == WordType.Open && target == WordType.Power)
        {

            OpenLocalPower();
            lines.Add("执行成功");
        }
        else if (action == WordType.Close && target == WordType.Power)
        {

            CloseLocalPower();
            lines.Add("执行成功");
        }
        else if (action == WordType.Open && target == WordType.Door)
        {

            OpenLocalDoor();
            if (ProcessSystem.Instance.IfPowerful == true) lines.Add("执行成功");
            else lines.Add("电力不足");
        }
        else if (action == WordType.Close && target == WordType.Door)
        {

            CloseLocalDoor();
            lines.Add("执行成功");
        }
        else
        {

            lines.Add("无效指令");
        }





        ClearCommand();
    }

    private void ClearCommand()
    {
        currentWords.Clear();
    }

    private string GetCurrentCommand()
    {
        string result = ">";

        foreach (WordType word in currentWords)
        {
            result += WordToChinese(word) + " ";
        }

        Debug.Log(result);
        return result;
    }

    private string WordToChinese(WordType word)
    {
        switch (word)
        {
            case WordType.Open: return "Kai";
            case WordType.Close: return "Guan";
            case WordType.Local: return "BenDi";
            case WordType.Power: return "Dian";
            case WordType.Light: return "Deng";
            case WordType.Door: return "Men";
            case WordType.Enter: return "ShuRu";
            default: return "错误";
        }
    }

    private void OpenLocalLight()
    {

        Debug.Log("执行：开 本地 灯");

        EventCenter.Instance.EventTrigger("OpenLocalLight");
    }

    private void CloseLocalLight()
    {
        Debug.Log("执行：关 本地 灯");
        EventCenter.Instance.EventTrigger("CloseLocalLight");
    }

    private void OpenLocalPower()
    {
        Debug.Log("执行：开 本地 供电");
        EventCenter.Instance.EventTrigger("OpenLocalPower");
    }

    private void CloseLocalPower()
    {
        Debug.Log("执行：关 本地 供电");
        EventCenter.Instance.EventTrigger("CloseLocalPower");
    }

    private void OpenLocalDoor()
    {
        Debug.Log("执行：开 本地 门");
        EventCenter.Instance.EventTrigger("OpenLocalDoor");
    }

    private void CloseLocalDoor()
    {
        Debug.Log("执行：关 本地 门");
        EventCenter.Instance.EventTrigger("CloseLocalDoor");
    }
}