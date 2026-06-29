using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputCommandSystem : Singleton<InputCommandSystem>
{
    public List<WordType> currentWords = new List<WordType>();
    public int currentLevel = 1;
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
        if (ProcessSystem.Instance.ifGetLowCard == false && currentLevel == 1 || ProcessSystem.Instance.ifGetMidCard == false && currentLevel == 2 || ProcessSystem.Instance.ifGetHighCard == false && currentLevel == 3)
        {
            lines.Add("权限卡缺失");
            ClearCommand();
            return;
        }
        if (currentWords.Count == 4)
        {
            lines.Add("无效指令");
            ClearCommand();
            return;
        }
        if (currentWords.Count <= 2)
        {
            if (currentWords.Count == 0 || currentWords.Count == 1)
            {
                lines.Add("无效指令");
                ClearCommand();
                return;
            }
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



        if ((action == WordType.Open || action == WordType.Close || action == WordType.Up || action == WordType.Down) &&
        (target == WordType.Light || target == WordType.Door || target == WordType.Oxygen || target == WordType.Temperature || target == WordType.Power))
        {
            string s = NoteSystem.Instance.WordToDisplayName(action) + NoteSystem.Instance.WordToDisplayName(range) + NoteSystem.Instance.WordToDisplayName(target);

            if (action == WordType.Open && target == WordType.Power)
            {
                EventCenter.Instance.EventTrigger(s); lines.Add("执行成功");
                Debug.Log(s);
            }
            else
                switch (range)
                {
                    case WordType.Dormi:

                        if (ProcessSystem.Instance.SuSheP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                    case WordType.Life:
                        if (ProcessSystem.Instance.WeiShengP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                    case WordType.Office:
                        if (ProcessSystem.Instance.BanGongP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                    case WordType.Electric:
                        if (ProcessSystem.Instance.DianLiP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                    case WordType.Research:
                        if (ProcessSystem.Instance.YanJiuP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                    case WordType.Coordi:
                        if (ProcessSystem.Instance.XinBiaoP == true) { EventCenter.Instance.EventTrigger(s); lines.Add("执行成功"); }
                        else { lines.Add("电力不足"); }
                        break;
                }

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
            result += NoteSystem.Instance.WordToDisplayName(word) + " ";
        }

        Debug.Log(result);
        return result;
    }



}