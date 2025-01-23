using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Rigidbody _rigidbody;

    private float _pitch;
    private float _boostSpeed;

    private Vector2 _movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var move =
            transform.forward * _movement.y
            + transform.right * _movement.x;
        move.y = 0;

        _rigidbody.AddForce(move.normalized *
                            (GameManager.Instance.movementSpeed + _boostSpeed),
                            ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        _movement = _context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext _context)
    {
        if (_context.performed)
            _boostSpeed = GameManager.Instance.BoostSpeed;
        else if (_context.canceled)
            _boostSpeed = 0.0f;
    }
}