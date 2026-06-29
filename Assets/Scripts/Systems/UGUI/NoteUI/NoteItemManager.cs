//统筹管理所有noteitem 词条ui

using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NotePanel : MonoBehaviour
{
    public NoteWordItem[] wordItems;

    public GameObject inputPanel;
    public TMP_InputField inputField;
    public TMP_Text pageFoot;

    private WordType currentEditingWord;

    public int currentPage = 0;
    private void Start()
    {
        currentPage = 0;
        for (int i = 7 * currentPage; i < NoteSystem.Instance.collectedWords.Count && i < 7 * currentPage + 7; i++)
        {
            wordItems[i % 7].Init(NoteSystem.Instance.collectedWords[i]);

        }
        RefreshAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Qtolast();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Etonext();
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
    public void Qtolast()
    {
        if (currentPage != 0)
        {
            currentPage--;
            pageFoot.text = currentPage + 1 + "/4";
            for (int i = 0; i < 7; i++)
            {
                wordItems[i].IsGive = false;
            }
            for (int i = 7 * currentPage; i < NoteSystem.Instance.collectedWords.Count && i < 7 * currentPage + 7; i++)
            {
                wordItems[i % 7].Init(NoteSystem.Instance.collectedWords[i]);

            }
            RefreshAll();
        }
    }
    public void Etonext()
    {
        if (currentPage != 3)
        {
            for (int i = 0; i < 7; i++)
            {
                wordItems[i].IsGive = false;
            }
            currentPage++;
            pageFoot.text = currentPage + 1 + "/4";
            for (int i = 7 * currentPage; i < NoteSystem.Instance.collectedWords.Count && i < 7 * currentPage + 7; i++)
            {
                wordItems[i % 7].Init(NoteSystem.Instance.collectedWords[i]);

            }
            RefreshAll();
        }


    }
}