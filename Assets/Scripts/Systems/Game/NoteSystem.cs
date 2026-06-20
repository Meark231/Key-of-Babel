using System.Collections.Generic;
using UnityEngine;

public class NoteSystem : Singleton<NoteSystem>
{
    // UI 可以直接读取这个显示

    // 已经收录的词
    public List<WordType> collectedWords = new List<WordType>();

    // 玩家自己的猜测
    public Dictionary<WordType, string> guesses = new Dictionary<WordType, string>();

    // 系统确认的真实含义
    public Dictionary<WordType, string> confirmedMeanings = new Dictionary<WordType, string>();
    private bool isopen = false;
    private void Start()
    {
        InitConfirmedMeanings();
        InitGuesses();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isopen == false)
        {
            UIManager.Instance.OpenPanel(UIConst.NotePanel);
            isopen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isopen == true)
        {
            UIManager.Instance.ClosePanel(UIConst.NotePanel);
            isopen = false;
        }
    }
    private void InitConfirmedMeanings()
    {
        confirmedMeanings[WordType.Open] = "开";
        confirmedMeanings[WordType.Close] = "关";
        confirmedMeanings[WordType.Local] = "本地";
        confirmedMeanings[WordType.Power] = "供电";
        confirmedMeanings[WordType.Light] = "灯";
        confirmedMeanings[WordType.Door] = "门";
        confirmedMeanings[WordType.Enter] = "输入";
    }
    private void InitGuesses()
    {
        guesses[WordType.Open] = "未猜测";
        guesses[WordType.Close] = "未猜测";
        guesses[WordType.Local] = "未猜测";
        guesses[WordType.Power] = "未猜测";
        guesses[WordType.Light] = "未猜测";
        guesses[WordType.Door] = "未猜测";
        guesses[WordType.Enter] = "未猜测";
    }
    public void CollectWord(WordType word)
    {
        if (collectedWords.Contains(word))
        {
            Debug.Log("已经收录过：" + WordToDisplayName(word));
            return;
        }

        collectedWords.Add(word);

        Debug.Log("收录新词：" + WordToDisplayName(word));


    }

    public void CollectWordById(string linkId)
    {
        if (System.Enum.TryParse(linkId, out WordType word))
        {
            CollectWord(word);
            Debug.Log("succeSs!");
        }
        else
        {
            Debug.LogWarning("无法识别的词 ID：" + linkId);
        }
    }

    public bool HasCollected(WordType word)
    {
        return collectedWords.Contains(word);
    }

    public void SetGuess(WordType word, string guess)
    {
        if (!collectedWords.Contains(word))
        {
            collectedWords.Add(word);
        }

        guesses[word] = guess;

    }

    public string GetMeaningText(WordType word)
    {
        if (!collectedWords.Contains(word))
        {
            return "未收录";
        }

        if (guesses.TryGetValue(word, out string guess))
        {
            return "猜测：" + guess;
        }

        return "含义未知";
    }

    public void ConfirmMeaning(WordType word)
    {
        if (!collectedWords.Contains(word))
        {
            collectedWords.Add(word);
        }

        if (confirmedMeanings.TryGetValue(word, out string meaning))
        {
            guesses[word] = meaning;
        }


    }



    public string WordToDisplayName(WordType word)
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
            default: return "未知";
        }
    }
}