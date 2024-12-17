using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference boostAction;
    [SerializeField] private InputActionReference fireAction;

    private const float Speed = 5.0f;

    private Rigidbody _rigidbody;

    private float _pitch;
    private float _boostSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        movementAction.action.Enable();
        boostAction.action.Enable();
        fireAction.action.Enable();

        boostAction.action.performed += _ => _boostSpeed = 5.0f;
        boostAction.action.canceled  += _ => _boostSpeed = 0.0f;

        fireAction.action.performed += _ => Shoot();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var movement       = movementAction.action.ReadValue<Vector2>();
        var forward = transform.forward * movement.y;
        var side   = transform.right   * movement.x;
        _rigidbody.linearVelocity = (forward + side).normalized * (Speed + _boostSpeed);
    }

    private void Shoot()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out var hit, 45f)) return;
        
        if (hit.transform.CompareTag("Enemy"))
        {
            Destroy(hit.transform.gameObject);
        }
    }
}