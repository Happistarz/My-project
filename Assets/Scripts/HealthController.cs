using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    [SerializeField] private TMP_Text      healthText;
    [SerializeField] private TMP_Text      titleText;

    private Camera _mainCamera;

    private Vector3 _offset = new(0, 1, 0);
    private float _health;
    private float _maxHealth;

    private Transform _target;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void InitHealthBar(float _initHealth, string _title, Vector3 _targetOffset, Transform _targetTransform)
    {
        _health    = _initHealth;
        _maxHealth = _initHealth;

        titleText.text = _title;
        _offset        = _targetOffset;
        _target        = _targetTransform;

        UpdateHealthUI();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateHealthUI();
    }

    public void SetHealth(float _newHealth)
    {
        _health = _newHealth;
        UpdateHealthUI();
    }

    public float GetHealth()
    {
        return _health;
    }

    private void UpdateHealthUI()
    {
        var screenPos = _mainCamera.WorldToScreenPoint(_target.transform.position + _offset);
        transform.position = screenPos;

        healthText.text = $"{_health}/{_maxHealth}";
        bar.localScale  = new Vector3(_health / _maxHealth, bar.localScale.y, bar.localScale.z);
    }
}