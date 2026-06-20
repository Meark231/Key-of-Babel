using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TMPLinkClickCollector : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text tmpText;
    private Canvas canvas;
    private Camera eventCamera;

    private void Awake()
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
            tmpText.ForceMeshUpdate();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tmpText == null) return;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            tmpText,
            eventData.position,
            eventCamera
        );

        if (linkIndex == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
        string linkId = linkInfo.GetLinkID();

        Debug.Log("点击了词：" + linkId);
        NoteSystem.Instance.CollectWordById(linkId);

        // 伪代码：这里以后加入笔记系统
        // NoteSystem.Instance.CollectWord(linkId);

        // 如果你后面用 enum：
        // if (System.Enum.TryParse(linkId, out WordType word))
        // {
        //     NoteSystem.Instance.CollectWord(word);
        // }
    }
}