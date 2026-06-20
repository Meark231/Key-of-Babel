using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum WordType
{
    Open,       // 开
    Close,      // 关
    Local,      // 本地
    Power,      // 供电
    Light,      // 灯
    Door,       // 门
    Enter       // 输入
}
public class WordButton : MonoBehaviour
{
    public TMP_Text screen;
    public WordType wordType;

    public void Press()
    {
        InputCommandSystem.Instance.InputWord(wordType);
        screen.text = InputCommandSystem.Instance.content;
    }
}
