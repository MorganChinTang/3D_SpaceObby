using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform targetToLookAt;
    public Vector3 offset;

    public Transform orientation;

    public float cameraRotateSpeed;

    float xRotation;
    float yRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetToLookAt.position + offset;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraRotateSpeed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraRotateSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
