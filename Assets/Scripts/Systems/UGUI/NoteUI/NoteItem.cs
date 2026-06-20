using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class NoteWordItem : MonoBehaviour
{
    private WordType wordType;
    private bool IsGive = false;
    public TMP_Text wordText;
    public TMP_Text guessText;
    public Button button;

    public NotePanel notePanel;

    public void Init(WordType wwordType)
    {
        wordType = wwordType;
        IsGive = true;


        Refresh();
    }

    public void Refresh()
    {
        if (IsGive) wordText.text = NoteSystem.Instance.WordToDisplayName(wordType);
        else wordText.text = "待写";




        if (string.IsNullOrEmpty(NoteSystem.Instance.guesses[wordType]))
        {
            guessText.text = "未猜测";
        }
        else
        {
            guessText.text = NoteSystem.Instance.guesses[wordType];
        }
    }

    public void OnClickItem()
    {
        Debug.Log("点击并作用函数");
        if (IsGive) notePanel.OpenGuessInput(wordType);
    }
}