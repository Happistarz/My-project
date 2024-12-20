using System.Collections;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
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
    private bool _isReloading = false;

    [SerializeField] private GameObject gun;
    [SerializeField] private TMP_Text   ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateAmmo();
    }

    public void Shoot()
    {
        if (_ammo <= 0 || _isReloading) return;

        Ammo--;
        
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hit, 45f) && hit.transform.CompareTag("Enemy"))
        {
            hit.transform.GetComponent<HealthManager>().TakeDamage(GameManager.Instance.DamageToEnemy);
        }

        if (_ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(GameManager.Instance.ReloadTime);
        Ammo        = 10;
        _isReloading = false;
    }

    private void UpdateAmmo()
    {
        ammoText.text = _ammo.ToString();
    }
}