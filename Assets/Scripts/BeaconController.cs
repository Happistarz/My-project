using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BeaconController : MonoBehaviour
{
    [SerializeField] private HealthManager        healthManager;
    [SerializeField] private InputActionReference beaconPowerAction;
    [SerializeField] private float                beaconPowerCooldown = 10.0f;
    [SerializeField] private BeaconPowerAnimation beaconPowerAnimation;

    [SerializeField] private Image beaconPowerImage;

    private float _beaconPowerTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        healthManager.OnDeath += OnDeath;

        beaconPowerAction.action.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        _beaconPowerTimer += Time.deltaTime;
        _beaconPowerTimer = Mathf.Clamp(_beaconPowerTimer, 0.0f, beaconPowerCooldown);
        UpdateBeaconPowerImage();
        
        if (_beaconPowerTimer >= beaconPowerCooldown)
        {
            beaconPowerImage.color = Color.green;
        }
        
        if (!beaconPowerAction.action.IsPressed() || !(_beaconPowerTimer >= beaconPowerCooldown)) return;
        _beaconPowerTimer = 0.0f;
        BeaconPower();
    }

    private void OnDeath()
    {
        Debug.Log("Beacon destroyed");
    }

    private void BeaconPower()
    {
        healthManager.TakeDamage(15f);
        beaconPowerImage.color = Color.white;

        beaconPowerAnimation.gameObject.SetActive(true);
    }
    
    private void UpdateBeaconPowerImage()
    {
        beaconPowerImage.transform.localScale = new Vector3(_beaconPowerTimer / beaconPowerCooldown, 1, 1);
    }
}