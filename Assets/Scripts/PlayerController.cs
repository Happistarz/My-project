using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference boostAction;
    [SerializeField] private InputActionReference fireAction;

    private Rigidbody _rigidbody;

    private float _pitch;
    private float _boostSpeed;
    
    private GunController _gunController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gunController = GetComponent<GunController>();

        movementAction.action.Enable();
        boostAction.action.Enable();
        fireAction.action.Enable();

        boostAction.action.performed += _ => _boostSpeed = GameManager.Instance.BoostSpeed;
        boostAction.action.canceled  += _ => _boostSpeed = 0.0f;

        fireAction.action.performed += _ => _gunController.Shoot();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var movement       = movementAction.action.ReadValue<Vector2>();
        var forward = transform.forward * movement.y;
        var side   = transform.right   * movement.x;
        _rigidbody.linearVelocity = (forward + side).normalized * (GameManager.Instance.MovementSpeed + _boostSpeed);
    }
}