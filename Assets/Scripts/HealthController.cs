using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    [SerializeField] private TMP_Text      healthText;
    [SerializeField] private TMP_Text      titleText;

    private Camera _mainCamera;

    private Vector3 _offset = new(0, 1, 0);

    private const float _MIN_SCALE = 0.5f;
    private const float _MAX_SCALE = 1.5f;

    private float _health;
    private float _maxHealth;

    private Transform _target;

    private Vector3 _normalScale;

    private void Awake()
    {
        _mainCamera  = Camera.main;
        _normalScale = transform.localScale;
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
        var screenPos = _mainCamera.WorldToScreenPoint(_target.transform.position + _offset);
        transform.position = screenPos;

        var distance = Vector3.Distance(_mainCamera.transform.position, _target.transform.position);

        if (screenPos.z <= 0 || distance > Constants.HEALTHBAR_MAX_DISTANCE)
        {
            transform.localScale = Vector3.zero;
            return;
        }

        transform.localScale = _normalScale;

        var scale = Mathf.Clamp(1 / distance, _MIN_SCALE, _MAX_SCALE);
        transform.localScale = new Vector3(scale, scale, scale);
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
        healthText.text = $"{_health}/{_maxHealth}";
        bar.localScale  = new Vector3(_health / _maxHealth, bar.localScale.y, bar.localScale.z);
    }
}