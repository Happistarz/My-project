using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public float initialHealth = 100;

    [SerializeField] private GameObject healthBarPrefab;

    private Canvas _canvas;

    public delegate void OnDeathEvent();

    public event OnDeathEvent OnDeath;

    public Vector3 offset = new(0, 1, 0);

    private HealthController _healthController;
    
    private void Awake()
    {
        _canvas = FindFirstObjectByType<Canvas>();
    }

    public void InitializeHealthBar(float _health, string _title, OnDeathEvent _onDeathCallback)
    {
        if (_canvas == null) return;

        _healthController = Instantiate(healthBarPrefab, _canvas.transform).GetComponent<HealthController>();
        _healthController.InitHealthBar(_health, _title, offset, transform);

        initialHealth = _health;
        
        OnDeath += _onDeathCallback;
    }

    public void TakeDamage(int _damage)
    {
        var health = _healthController.GetHealth();
        health -= _damage;
        if (health <= 0)
        {
            Destroy(_healthController.gameObject);
            OnDeath?.Invoke();
        }

        _healthController.SetHealth(health);
    }
}