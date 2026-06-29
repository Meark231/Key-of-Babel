using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
public class TMPLinkClickCollector : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler, IPointerExitHandler
{
    private TMP_Text tmpText;
    private Canvas canvas;
    private Camera eventCamera;
    private string originalText;
    private int currentHoverLinkIndex = -1;
    [Header("悬停提示 UI")]
    public GameObject guessUIPrefab;
    public Vector2 tooltipOffset = new Vector2(0, 60);

    private GameObject guessUIInstance;
    private TMP_Text guessTMP;
    private RectTransform guessRect;

    private void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            eventCamera = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;
        }
        else
        {
            eventCamera = null;
        }


        if (tmpText != null)
        {
            originalText = tmpText.text;
            tmpText.ForceMeshUpdate();
        }
        CreateGuessUI();
    }
    void OnDestroy()
    {
        Destroy(guessUIInstance);
    }
    private void CreateGuessUI()
    {
        if (guessUIPrefab == null)
        {
            Debug.LogError("guessUIPrefab 没拖");
            return;
        }

        if (canvas == null)
        {
            Debug.LogError("找不到 Canvas");
            return;
        }

        guessUIInstance = Instantiate(guessUIPrefab, canvas.transform);
        guessRect = guessUIInstance.GetComponent<RectTransform>();

        Transform content = guessUIInstance.transform.Find("content");
        if (content == null)
        {
            Debug.LogError("guessUI 预制体下面找不到 content");
            return;
        }

        guessTMP = content.GetComponent<TMP_Text>();
        if (guessTMP == null)
        {
            Debug.LogError("content 上没有 TMP_Text");
            return;
        }

        guessUIInstance.SetActive(false);
    }
    public void RefreshOriginalText()
    {
        if (tmpText == null)
        {
            tmpText = GetComponent<TMP_Text>();
        }

        originalText = tmpText.text;
        currentHoverLinkIndex = -1;
        tmpText.ForceMeshUpdate();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (tmpText == null) return;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            tmpText,
            eventData.position,
            eventCamera
        );

        if (linkIndex == -1) return;

        TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
        string linkId = linkInfo.GetLinkID();

        Debug.Log("点击了词：" + linkId);
        NoteSystem.Instance.CollectWordById(linkId);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (tmpText == null || guessUIInstance == null) return;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            tmpText,
            eventData.position,
            eventCamera
        );

        if (linkIndex == -1)
        {
            guessUIInstance.SetActive(false);
            ClearUnderline();
            return;
        }

        if (linkIndex != currentHoverLinkIndex)
        {
            currentHoverLinkIndex = linkIndex;
            ApplyUnderline(linkIndex);
        }

        TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
        string linkId = linkInfo.GetLinkID();

        ShowGuessUI(linkId, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (guessUIInstance != null)
        {
            guessUIInstance.SetActive(false);
        }

        ClearUnderline();
    }
    private void ShowGuessUI(string linkId, Vector2 mouseScreenPos)
    {
        if (!System.Enum.TryParse(linkId, out WordType word))
        {
            guessTMP.text = "未知词条";
        }
        else
        {
            if (NoteSystem.Instance.guesses.TryGetValue(word, out string guess) &&
                !string.IsNullOrEmpty(guess))
            {
                guessTMP.text = guess;
            }
            else
            {
                guessTMP.text = "未猜测";
            }
        }

        guessUIInstance.SetActive(true);
        UpdateGuessUIPosition(mouseScreenPos);
    }

    private void UpdateGuessUIPosition(Vector2 mouseScreenPos)
    {
        if (guessRect == null || canvas == null) return;

        RectTransform canvasRect = canvas.transform as RectTransform;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mouseScreenPos + tooltipOffset,
            eventCamera,
            out localPoint
        );

        guessRect.anchoredPosition = localPoint;
    }
    private void ApplyUnderline(int targetLinkIndex)
    {
        if (string.IsNullOrEmpty(originalText)) return;

        int currentIndex = 0;

        string pattern = @"<link=""([^""]+)"">(.*?)</link>";

        string result = Regex.Replace(
            originalText,
            pattern,
            match =>
            {
                if (currentIndex == targetLinkIndex)
                {
                    currentIndex++;
                    return "<link=\"" + match.Groups[1].Value + "\"><u>" + match.Groups[2].Value + "</u></link>";
                }

                currentIndex++;
                return match.Value;
            },
            RegexOptions.Singleline
        );

        tmpText.text = result;
        tmpText.ForceMeshUpdate();
    }

    private void ClearUnderline()
    {
        if (currentHoverLinkIndex == -1) return;

        currentHoverLinkIndex = -1;

        if (!string.IsNullOrEmpty(originalText))
        {
            tmpText.text = originalText;
            tmpText.ForceMeshUpdate();
        }
    }

}