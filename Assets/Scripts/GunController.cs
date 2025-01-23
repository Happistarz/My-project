using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private static readonly  int                  _SHOOT = Animator.StringToHash("Shoot");
    [SerializeField] private GameObject           bulletPrefab;
    [SerializeField] private Transform            bulletSpawnPoint;
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private Animator             gunAnimator;

    [SerializeField] private TMP_Text ammoText;

    private int _ammo = 10;

    private int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = value;
            UpdateAmmo();
        }
    }

    private bool  _isReloading;
    private float _fireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateAmmo();

        shootAction.action.Enable();
    }

    private void Update()
    {
        if (!shootAction.action.IsPressed() || !(Time.time >= _fireTime)) return;

        _fireTime = Time.time + GameManager.Instance.fireRate;
        Shoot();
    }

    private void Shoot()
    {
        if (_ammo <= 0 || _isReloading) return;

        Ammo--;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * BulletController.SPEED;
        gunAnimator.Play("Gun");

        if (_ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(GameManager.Instance.ReloadTime);
        Ammo         = 10;
        _isReloading = false;
    }

    private void UpdateAmmo()
    {
        ammoText.text = _ammo.ToString();
    }
}