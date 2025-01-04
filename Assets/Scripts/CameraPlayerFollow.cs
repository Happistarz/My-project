using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float     sensitivity = 0.1f;

    private float _pitch;
    
    private Vector2 _look;

    // LateUpdate is called once per frame after all Update functions have been called
    private void LateUpdate()
    {
        if (Time.timeScale == 0) return; // The game is paused, don't update the camera
        
        transform.Rotate(Vector3.up, _look.x * sensitivity * Time.deltaTime);
        playerBody.Rotate(Vector3.up, _look.x * sensitivity * Time.deltaTime);
        
        _pitch -= _look.y * sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -45, 45);
        transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);
    }
    
    public void OnLook(InputAction.CallbackContext _context)
    {
        _look = _context.ReadValue<Vector2>();
    }
}