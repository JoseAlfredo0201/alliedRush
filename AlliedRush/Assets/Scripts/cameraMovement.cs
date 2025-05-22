using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player; // The target object to follow (e.g., the player)
    public float smoothSpeed = 0.125f; // The speed at which the camera follows the target
    public Vector3 offset; // The offset position of the camera relative to the target

    void LateUpdate()
    {
        if (Player != null) // Check if the target is assigned
        {
            Vector3 desiredPosition = Player.position + offset; // Calculate the desired position of the camera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smoothly interpolate between current and desired position
            transform.position = smoothedPosition; // Update the camera's position
        }
    }
}