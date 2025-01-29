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

    private void FixedUpdate()
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
        // look at beacon and move towards it (not in the y-axis)
        var direction = beacon.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        
        transform.Translate(0, 0, EnemyProperties.Speed * Time.deltaTime);
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
        EnemyWaveController.Instance.EnemyDied();
        GameManager.Instance.Score += EnemyProperties.Score;
        Destroy(gameObject);
    }
}