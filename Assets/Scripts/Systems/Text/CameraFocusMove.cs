using UnityEngine;
using DG.Tweening;

public class CameraFocusMove : Singleton<CameraFocusMove>
{
    public Transform player;
    public PlayerMove playerMove;

    public float focusDuration = 0.45f;
    public float backDuration = 0.35f;

    public Ease focusEase = Ease.OutCubic;
    public Ease backEase = Ease.OutCubic;

    private Vector3 offset;
    private bool isFocusing = false;

    private void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
        }

        if (playerMove == null && player != null)
        {
            playerMove = player.GetComponent<PlayerMove>();
        }
    }

    public void FocusToPlayer()
    {
        if (player == null) return;

        transform.DOKill();

        isFocusing = true;

        if (playerMove != null)
            playerMove.ifsearch = true;

        // 正常跟随时摄像机应该在的位置
        Vector3 normalCameraPos = player.position + offset;

        // 玩家位置和摄像机位置的中点
        Vector3 targetPos = (normalCameraPos + player.position) * 0.5f;

        transform
            .DOMove(targetPos, focusDuration)
            .SetEase(focusEase);
    }

    public void BackToNormal()
    {
        if (player == null) return;

       transform.DOKill();

        Vector3 normalCameraPos = player.position + offset;

        transform
            .DOMove(normalCameraPos, backDuration)
            .SetEase(backEase)
            .OnComplete(() =>
            {
                isFocusing = false;

                if (playerMove != null)
                    playerMove.ifsearch = false;
            });
    }

    private void LateUpdate()
    {
        if (player == null) return;
        if (isFocusing) return;

        transform.position = player.position + offset;
    }
}