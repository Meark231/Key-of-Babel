using UnityEngine;

public class TableDian : MonoBehaviour
{
    public SpriteRenderer sr;
    private Material mat;
    public TextAsset dialogText;
    public GameObject TipsE;
    public bool isopen = false;
    private bool canInteract = false;
    private bool iffirst = true;
    public string clueName;

    private void Start()
    {

        mat = sr.material;

    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F) && isopen == false && PlayerState.Instance.currentps == PlayerState.ps.Movable)
        {
            PlayerState.Instance.currentps = PlayerState.ps.ReadingPanel;
            CluesSystem.Instance.OpenClue(clueName);

            isopen = true;
        }
        else if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F) && isopen == true)
        {
            PlayerState.Instance.currentps = PlayerState.ps.Movable;
            isopen = false;
            UIManager.Instance.ClosePanel(UIConst.CluePanel);
            if (iffirst) ItemsSystem.Instance.ownitems.Add(clueName);
            iffirst = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = true;

            mat.SetFloat("_OutlineThickness", 0.004f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interactRange"))
        {
            canInteract = false;

            mat.SetFloat("_OutlineThickness", 0.004f);
        }
    }
}
