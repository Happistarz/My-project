using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        NORMAL,
        FAST,
        TANK,
        DAGGER,
        LENGTH
    }

    public EnemyTypes EnemyProperties;

    [SerializeField] public Transform beacon;

    [SerializeField] private const float AttackRange = 3.0f;

    private float _attackTimer = 0.0f;

    [SerializeField] private HealthManager healthManager;

    // Update is called once per frame
    private void Start()
    {
        healthManager.SetInitialHealth(EnemyProperties.Health);

        healthManager.OnDeath += OnDeath;
    }

    private void Update()
    {
        if (beacon == null) return;

        var distance = Vector3.Distance(beacon.position, transform.position);

        if (distance > AttackRange)
            MoveToBeacon();
        else
            Attack();
    }

    private void MoveToBeacon()
    {
        var direction = beacon.position - transform.position;
        direction.y = 0;
        transform.Translate(direction.normalized * (EnemyProperties.Speed * Time.deltaTime));
    }

    private void Attack()
    {
        _attackTimer += Time.deltaTime;
        if (!(_attackTimer >= EnemyProperties.AttackSpeed)) return;

        _attackTimer = 0.0f;
        EnemyProperties.Attack(beacon.GetComponent<HealthManager>());
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        Destroy(healthManager.gameObject);
    }
}