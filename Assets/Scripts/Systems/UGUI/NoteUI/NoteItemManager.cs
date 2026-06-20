using UnityEngine;
using TMPro;

public class NotePanel : MonoBehaviour
{
    public NoteWordItem[] wordItems;

    public GameObject inputPanel;
    public TMP_InputField inputField;

    private WordType currentEditingWord;

    private void Start()
    {
        for (int i = 0; i < NoteSystem.Instance.collectedWords.Count; i++)
        {
            wordItems[i].Init(NoteSystem.Instance.collectedWords[i]);
            RefreshAll();
        }

    }



    public void RefreshAll()
    {
        for (int i = 0; i < wordItems.Length; i++)
        {
            wordItems[i].Refresh();
        }
    }

    public void OpenGuessInput(WordType word)
    {
        Debug.Log("调用了打开函数");
        currentEditingWord = word;

        inputPanel.SetActive(true);

        string oldGuess = NoteSystem.Instance.guesses[word];
        inputField.text = oldGuess;

        inputField.ActivateInputField();
    }

    public void ConfirmGuess()
    {
        string guess = inputField.text;

        NoteSystem.Instance.SetGuess(currentEditingWord, guess);

        inputPanel.SetActive(false);

        RefreshAll();
    }

    public void CancelGuess()
    {
        inputPanel.SetActive(false);
    }
}