using UnityEngine;

public class CameraSmooth : MonoBehaviour
{    
    public Transform Player;    
    public Vector3 offset;
    public float smoothSpeed;
    
    private void LateUpdate()
    {
        Vector3 desiredPosition = Player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Prespective Effect
        // transform.LookAt(Player);
    }
}