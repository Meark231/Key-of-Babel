using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum WordType
{
    Open,       // 开
    Close,      // 关
    Power,      // 供电
    Light,      // 灯
    Door,       // 门
    Enter,       // 输入
    Oxygen,
    Temperature,
    Up,
    Down,
    Dormi,
    Life,
    Office,
    Electric,
    Research,
    Coordi


}
public class WordButton : MonoBehaviour
{
   

    public TMP_Text screen;
    public WordType wordType;

    public void Press()
    {
        AudioSystem.Instance.PlayButton();
        InputCommandSystem.Instance.InputWord(wordType);
        screen.text = InputCommandSystem.Instance.content;
    }
}
