using UnityEngine;

public class TableInteract : MonoBehaviour
{
    public GameObject outlineObj;
    public TextAsset dialogText;
    public GameObject TipsE;
    private bool canInteract = false;

    private void Start()
    {

        outlineObj.SetActive(false);

    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F))
        {
            DialogSystem.Instance.say(dialogText);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = true;

            if (outlineObj != null)
            {
                TipsE.SetActive(true);
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
                TipsE.SetActive(false);
                outlineObj.SetActive(false);
            }
        }
    }
}
