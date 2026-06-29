using UnityEngine;
using UnityEngine.Playables;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float acceleration = 35f;
    public float deceleration = 45f;

    public float cameraSmoothTime = 0.12f;
    public bool ifsearch = false;

    private Rigidbody rb;
    private Vector3 inputDir;
    private Vector3 currentVelocity;

    private Vector3 cameraOffset;
    private Vector3 cameraVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;

        if (Camera.main != null)
        {
            cameraOffset = Camera.main.transform.position - transform.position;
        }
    }

    private void Update()
    {
        if (PlayerState.Instance.currentps == PlayerState.ps.Movable)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputZ = Input.GetAxisRaw("Vertical");

            inputDir = new Vector3(inputX, 0f, inputZ).normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = inputDir * maxSpeed;

        float changeSpeed = inputDir.sqrMagnitude > 0.01f ? acceleration : deceleration;

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            changeSpeed * Time.fixedDeltaTime
        );

        rb.velocity = currentVelocity;
    }

    private void LateUpdate()
    {
        if (Camera.main != null && ifsearch == false)
        {
            Vector3 targetCameraPos = transform.position + cameraOffset;

            Camera.main.transform.position = Vector3.SmoothDamp(
                Camera.main.transform.position,
                targetCameraPos,
                ref cameraVelocity,
                cameraSmoothTime
            );
        }
    }
}