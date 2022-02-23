using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float MovementSpeed;
    public float CameraSpeed;

    private Vector3 processedInput = new Vector3(0, 0, 0);
    float mouseInputX;
    float mouseInputY;

    private bool canJump = false;

    private Rigidbody rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");//左右，左-1，右1
        var verticalInput = Input.GetAxisRaw("Vertical");//前后

        mouseInputX = Input.GetAxisRaw("Mouse X");
        mouseInputY = Input.GetAxisRaw("Mouse Y");

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            anim.SetBool("Jumping", true);
        }

        processedInput = (transform.forward * verticalInput + transform.right * horizontalInput);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("Moving", true);

            if (verticalInput == 1)
            {
                anim.SetBool("Forward", true);
                anim.SetBool("Backward", false);
            }
            else if (verticalInput == -1)
            {
                anim.SetBool("Forward", false);
                anim.SetBool("Backward", true);
            }
            else if (verticalInput == 0)
            {
                anim.SetBool("Forward", false);
                anim.SetBool("Backward", false);
            }

            if (horizontalInput == 1)
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
            }
            else if (horizontalInput == -1)
            {
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
            }
            else if (horizontalInput == 0)
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
            }
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (processedInput.normalized * MovementSpeed * Time.deltaTime));

        transform.Rotate(new Vector3(0, mouseInputX, 0) * CameraSpeed, Space.Self);
        transform.GetChild(4).transform.Rotate(new Vector3(-mouseInputY, 0, 0) * CameraSpeed, Space.Self);

        if (CheckAngle(transform.GetChild(4).transform.localEulerAngles.x) < -60)
        {
            transform.GetChild(4).transform.localEulerAngles = new Vector3(-60, 0, 0);
        }
        if (CheckAngle(transform.GetChild(4).transform.localEulerAngles.x) > 60)
        {
            transform.GetChild(4).transform.localEulerAngles = new Vector3(60, 0, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Jumping", false);
    }
    private void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }
    public float CheckAngle(float Value)
    {
        float Angle = Value - 180;
        if (Angle > 0)
        {
            return Angle - 180;
        }
        return Angle + 180;
    }
}