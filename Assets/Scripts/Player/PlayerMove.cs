/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

private float inputX,inputY;    
private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset=Camera.main.transform.position-transform.position;
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX=Input.GetAxisRaw("Horizontal");
        inputY=Input.GetAxisRaw("Vertical");
        Vector2 input=new Vector2(inputX,inputY).normalized;
        rb.velocity=input*speed;
        Camera.main.transform.position=transform.position+offset;
    }
}*/
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody rb;
    private Vector3 moveDir;
    private Vector3 cameraOffset;
    public bool ifsearch = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (Camera.main != null)
        {
            cameraOffset = Camera.main.transform.position - transform.position;
        }

        // 防止角色被物理撞歪
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A=-1, D=1
        float inputZ = Input.GetAxisRaw("Vertical");   // S=-1, W=1

        // XZ 平面移动：D 是 X+，W 是 Z+
        moveDir = new Vector3(inputX, 0f, inputZ).normalized;

        if (Camera.main != null && ifsearch == false)
        {
            Camera.main.transform.position = transform.position + cameraOffset;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed;
    }
}