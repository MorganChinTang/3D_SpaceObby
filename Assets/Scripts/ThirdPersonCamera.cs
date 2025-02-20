using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation = null;
    public float smoothFactor = 10.0f;
    public Vector3 offset = Vector3.zero;
    public Quaternion lookOffset = Quaternion.identity;

    private void Update()
    {
        Vector3 targetPosition = orientation.position + (orientation.right * offset.x) 
            + (orientation.up * offset.y) + (orientation.forward * offset.z);
        Quaternion targetRotation = Quaternion.LookRotation(orientation.forward);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothFactor * Time.deltaTime);
    }
}
