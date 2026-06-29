using UnityEngine;

public class TableInteract : MonoBehaviour
{
    public SpriteRenderer sr;
    private Material mat;

    public TextAsset dialogText;
    public GameObject TipsE;
    public bool isopen = false;
    private bool canInteract = false;
    private bool iffirst = true;

    private void Start()
    {
        mat = sr.material;

    }

    private void Update()
    {
        if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F) && isopen == false && PlayerState.Instance.currentps == PlayerState.ps.Movable)
        {
            PlayerState.Instance.currentps = PlayerState.ps.ReadingPanel;
            CluesSystem.Instance.OpenClue("Buttons");

            isopen = true;
        }
        else if (canInteract && DialogSystem.Instance.ifReading == false && Input.GetKeyDown(KeyCode.F) && isopen == true)
        {
            PlayerState.Instance.currentps = PlayerState.ps.Movable;
            isopen = false;
            UIManager.Instance.ClosePanel(UIConst.CluePanel);
            if (iffirst == true)
            {
                DialogSystem.Instance.sayDirect("我", "或许我该推测一下这些词的意思，左键单击蓝色字体以加入词条笔记，可以用tab打开并编辑揣测的意思");
                ItemsSystem.Instance.ownitems.Add("Buttons");
            }
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

            mat.SetFloat("_OutlineThickness", 0f);
        }
    }
}
