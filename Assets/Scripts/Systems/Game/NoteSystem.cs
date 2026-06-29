using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
        if (Input.GetKeyDown(KeyCode.Tab) && isopen == false && PlayerState.Instance.currentps == PlayerState.ps.Movable)
        {
            PlayerState.Instance.currentps = PlayerState.ps.ReadingNote;
            UIManager.Instance.OpenPanel(UIConst.NotePanel);
            isopen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isopen == true)
        {
            PlayerState.Instance.currentps=PlayerState.ps.Movable;
            UIManager.Instance.ClosePanel(UIConst.NotePanel);
            isopen = false;
        }
    }
    private void InitConfirmedMeanings()
    {
        confirmedMeanings[WordType.Open] = "开";
        confirmedMeanings[WordType.Close] = "关";
        confirmedMeanings[WordType.Dormi] = "本地";
        confirmedMeanings[WordType.Power] = "供电";
        confirmedMeanings[WordType.Light] = "灯";
        confirmedMeanings[WordType.Door] = "门";
        confirmedMeanings[WordType.Enter] = "输入";
        confirmedMeanings[WordType.Oxygen] = "供氧系统";
        confirmedMeanings[WordType.Temperature] = "温度系统";
        confirmedMeanings[WordType.Up] = "升高";
        confirmedMeanings[WordType.Down] = "降低";
        confirmedMeanings[WordType.Dormi] = "宿舍区";
        confirmedMeanings[WordType.Life] = "维生舱";
        confirmedMeanings[WordType.Office] = "办公区";
        confirmedMeanings[WordType.Electric] = "电力区";
        confirmedMeanings[WordType.Research] = "研究舱";
        confirmedMeanings[WordType.Coordi] = "信标舱";
    }
    private void InitGuesses()
    {
        guesses[WordType.Open] = "未猜测";
        guesses[WordType.Close] = "未猜测";
        guesses[WordType.Dormi] = "未猜测";
        guesses[WordType.Power] = "未猜测";
        guesses[WordType.Light] = "未猜测";
        guesses[WordType.Door] = "未猜测";
        guesses[WordType.Enter] = "未猜测";
        guesses[WordType.Oxygen] = "未猜测";
        guesses[WordType.Temperature] = "未猜测";
        guesses[WordType.Up] = "未猜测";
        guesses[WordType.Down] = "未猜测";
        guesses[WordType.Dormi] = "未猜测";
        guesses[WordType.Life] = "未猜测";
        guesses[WordType.Office] = "未猜测";
        guesses[WordType.Electric] = "未猜测";
        guesses[WordType.Research] = "未猜测";
        guesses[WordType.Coordi] = "未猜测";
    }
    public void CollectWord(WordType word)
    {
        if (collectedWords.Contains(word))
        {

            return;
        }
        collectedWords.Add(word);
    }

    public void CollectWordById(string linkId)
    {
        if (System.Enum.TryParse(linkId, out WordType word))
        {
            CollectWord(word);
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
            case WordType.Dormi: return "SuShe";
            case WordType.Power: return "Dian";
            case WordType.Light: return "Deng";
            case WordType.Door: return "Men";
            case WordType.Enter: return "ShuRu";
            case WordType.Oxygen: return "Yang";
            case WordType.Temperature: return "WenDu";
            case WordType.Up: return "Gao";
            case WordType.Down: return "Di";
            case WordType.Life: return "WeiSheng";
            case WordType.Office: return "BanGong";
            case WordType.Electric: return "DianLi";
            case WordType.Research: return "Yanjiu";
            case WordType.Coordi: return "XinBiao";
            default: return "未知";
        }
    }
}