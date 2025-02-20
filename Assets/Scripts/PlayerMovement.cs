using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    //References:
    public Transform orientation;
    Rigidbody rb;
    [SerializeField]
    Animator animator;

    public GameObject welldone;

    public float moveSpeed;
    public float jumpForce;
    public float groundDrag;
    public float rotateSpeed;

    float horizontalInput;
    float verticalInput;
    float rotationInput;
    float yRotation;
    Vector3 moveDirection;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        GameController.Instance.SetPlayerStartPosition(gameObject);
        welldone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotationInput = Input.GetAxisRaw("Mouse X");

        yRotation += rotationInput * Time.deltaTime * rotateSpeed;
        orientation.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);

        animator.SetFloat("Walk", verticalInput);
        animator.SetFloat("Strafe", horizontalInput);
        animator.SetBool("IsMoving", (Mathf.Abs(horizontalInput) > 0.0f || Mathf.Abs(verticalInput) > 0.0f));
        //Process Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        //Physics based movement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);

        //SPEED LIMIT
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
        }

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("On Ground");
        }

        if (other.CompareTag("Finish"))
        {
            Hud.StopTimer();
            welldone.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("Off Ground");
        }
            
    }

}
