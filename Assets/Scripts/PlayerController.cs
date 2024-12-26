using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference boostAction;
    
    [SerializeField] private GunController gunController;

    private Rigidbody _rigidbody;

    private float _pitch;
    private float _boostSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        movementAction.action.Enable();
        boostAction.action.Enable();

        boostAction.action.performed += _ => _boostSpeed = GameManager.Instance.BoostSpeed;
        boostAction.action.canceled  += _ => _boostSpeed = 0.0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var movement = movementAction.action.ReadValue<Vector2>();
        var forward  = transform.forward * movement.y;
        var side     = transform.right   * movement.x;
        _rigidbody.linearVelocity = (forward + side).normalized * (GameManager.Instance.movementSpeed + _boostSpeed);
    }
}