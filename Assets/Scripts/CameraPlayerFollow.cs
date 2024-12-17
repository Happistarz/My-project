using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;

    [SerializeField] private Transform player;
    [SerializeField] private float     Sensitivity = 0.1f;

    private float _pitch = 0;
    private void Start()
    {
        lookAction.action.Enable();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var look = lookAction.action.ReadValue<Vector2>();

        // _yaw   += look.x * Sensitivity;
        // _pitch -= look.y * Sensitivity;
        // _pitch =  Mathf.Clamp(_pitch, -45, 45);
        //
        // Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        // transform.position = player.position + rotation * offset;
        // transform.LookAt(player.position);
        // transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
        // player.rotation    = Quaternion.Euler(0,      _yaw, 0);
        
        float yaw = look.x * Sensitivity;
        float pitch = look.y * Sensitivity;
        
        _pitch -= pitch;
        _pitch = Mathf.Clamp(_pitch, -45, 45);
        
        transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        player.Rotate(Vector3.up, yaw);
    }
}