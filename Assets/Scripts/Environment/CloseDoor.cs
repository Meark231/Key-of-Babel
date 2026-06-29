using DG.Tweening;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject outlineObj;

    public Sprite closeDoorSprite;
    private bool canInteract = false;
    private bool isOpen = false;
    private SpriteRenderer sr;
    private BoxCollider doorCollider;
    public GameObject opendoor;
    public Transform cameraTransform;
    public string kaievent;
    public string guanevent;
    private void Start()
    {

        outlineObj.SetActive(false);
        EventCenter.Instance.AddEventListener(kaievent, OpenDoor);
        EventCenter.Instance.AddEventListener(guanevent, CloseDoorw);
        sr = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F) && isOpen == false)
        {
            DialogSystem.Instance.sayDirect("我", "大门看起来关的严丝合缝，也许哪里有<link=\"Local\"><color=#66ccff>BenDi</color></link>");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = true;

            if (outlineObj != null && isOpen == false)
            {
                outlineObj.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = false;

            if (outlineObj != null)
            {
                outlineObj.SetActive(false);
            }
        }
    }
    private void OpenDoor()
    {
        AudioSystem.Instance.PlayDoor();
        if (isOpen) return;
        cameraTransform.DOKill();

        cameraTransform.DOShakePosition(
            duration: 0.4f,
            strength: 0.2f,
            vibrato: 20,
            randomness: 90f,
            snapping: false,
            fadeOut: true
        );
        isOpen = true;
        sr.sprite = null;
        doorCollider.enabled = false;
        opendoor.SetActive(true);
    }
    private void CloseDoorw()
    {
        if (isOpen == false) return;
        cameraTransform.DOKill();

        cameraTransform.DOShakePosition(
            duration: 0.4f,
            strength: 0.2f,
            vibrato: 20,
            randomness: 90f,
            snapping: false,
            fadeOut: true
        );
        isOpen = false;
        sr.sprite = closeDoorSprite;
        doorCollider.enabled = true;
        opendoor.SetActive(false);
    }
}
