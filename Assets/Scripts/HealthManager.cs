using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public float health = 100.0f;

    [SerializeField] private GameObject healthBarPrefab;

    private Canvas _canvas;
    private Camera _mainCamera;

    public delegate void OnDeathEvent();

    public event OnDeathEvent OnDeath;

    public Vector3 offset = new(0, 1, 0);

    private RectTransform _healthBar;

    private RectTransform _healthRect;
    private TMP_Text      _healthText;

    private float _maxHealth;

    // Start is called before the first frame update
    private void Awake()
    {
        _mainCamera = Camera.main;
        _canvas     = FindFirstObjectByType<Canvas>();

        _healthBar  = Instantiate(healthBarPrefab, _canvas.transform).GetComponent<RectTransform>();
        _healthRect = _healthBar.Find("Health").GetComponent<RectTransform>();
        _healthText = _healthBar.Find("Text").GetComponent<TMP_Text>();

        SetInitialHealth(health);
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
        health -= _damage;
        if (health <= 0)
        {
            Destroy(_healthBar.gameObject);
            OnDeath?.Invoke();
        }

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _healthRect.localScale = new Vector3(health / _maxHealth, _healthRect.localScale.y, _healthRect.localScale.z);
        _healthText.text       = $"{health:0.0}";
    }

    public void SetInitialHealth(float _health)
    {
        health     = _health;
        _maxHealth = _health;
        UpdateHealthUI();
    }
}