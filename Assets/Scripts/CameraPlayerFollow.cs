using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3   offset;
    [SerializeField] private float     Sensitivity = 0.1f;

    private const float SmoothSpeed = 0.125f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        lookAction.action.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        var desiredPosition  = player.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;

        var look = lookAction.action.ReadValue<Vector2>() * Sensitivity;

        yaw   += look.x;
        pitch -= look.y;
        pitch =  Mathf.Clamp(pitch, -90, 90);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        player.rotation    = Quaternion.Euler(0,     yaw, 0);
    }
}