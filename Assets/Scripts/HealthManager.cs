using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float Health { get; private set; } = 100.0f;

    [SerializeField] private GameObject healthBarPrefab;

    private Canvas        _canvas;
    private Camera        _mainCamera;

    public Vector3 offset = new Vector3(0, 1, 0);

    private RectTransform _healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _mainCamera = Camera.main;
        _canvas = FindFirstObjectByType<Canvas>();
        
        _healthBar = Instantiate(healthBarPrefab, _canvas.transform).GetComponent<RectTransform>();
        
        UpdateHealthUI();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var screenPos = _mainCamera.WorldToScreenPoint(transform.position + offset);
        _healthBar.position = screenPos;
        _healthBar.gameObject.SetActive(screenPos.z > 0);
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
            Destroy(_healthBar.gameObject);
        }

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _healthBar.localScale = new Vector3(Health / 100.0f, _healthBar.localScale.y, _healthBar.localScale.z);
    }
}