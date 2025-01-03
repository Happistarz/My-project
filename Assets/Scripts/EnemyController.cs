using UnityEngine;

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

    public Transform beacon;

    private const float _ATTACK_RANGE = 3.0f;

    private float _attackTimer;

    [SerializeField] private HealthManager healthManager;

    // Update is called once per frame
    private void Start()
    {
        healthManager.InitializeHealthBar(EnemyProperties.Health, EnemyProperties.Type.ToString(), OnDeath);
    }

    private void Update()
    {
        if (beacon == null) return;

        var distance = Vector3.Distance(beacon.position, transform.position);

        if (distance > _ATTACK_RANGE)
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
        GameManager.Instance.EnemyWaveController.EnemyDied();
        Destroy(gameObject);
    }
}