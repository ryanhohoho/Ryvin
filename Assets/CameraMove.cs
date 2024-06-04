using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

    private void LateUpdate()
    {
        // Get the mouse's y-axis input
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        // Add the input to the xRotation variable, but clamp it to prevent excessive rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Apply the xRotation to the camera's local rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Lerp the camera's position to the target's position with added distance and height
        transform.position = Vector3.Lerp(transform.position, target.position - target.forward * distance + Vector3.up * height, Time.deltaTime * followSpeed);
        // Lerp the camera's rotation to match the target's rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), Time.deltaTime * rotationSpeed);
    }
}
/* 360 FOLLOWING PLAYER CODE BUT NO PANNING UP AND DOWN
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    public float distance = 5f;
    public float height = 2f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - target.forward * distance + Vector3.up * height, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), Time.deltaTime * rotationSpeed);
    }
}
*/