using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using DG.Tweening;
public class Button1Panel : BasePanel
{
    private RectTransform rectTransform;
    private Vector2 originPos;
    public Material crtMaterial;
    private static readonly int CRTStrengthID = Shader.PropertyToID("_strength");
    // Start is called before the first frame update
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition;
    }
    void Start()
    {
        crtMaterial.SetFloat(CRTStrengthID, 0f);
        rectTransform.DOKill();

        // 先把 UI 放到原位置下方 120 像素
        rectTransform.anchoredPosition = originPos + new Vector2(0, -480f);

        // 再滑回原位置
        rectTransform
            .DOAnchorPos(originPos, 0.7f)
            .SetEase(Ease.OutCubic);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {
        crtMaterial.SetFloat(CRTStrengthID, 1f);
    }

}
