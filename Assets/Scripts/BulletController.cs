using UnityEngine;

public class BulletController : MonoBehaviour
{
    public const             float Speed    = 30.0f;
    [SerializeField] private float lifeTime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (!_other.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            Destroy(gameObject);
            return;
        }

        _other.gameObject.GetComponent<HealthManager>().TakeDamage(GameManager.Instance.DamageToEnemy);
        Destroy(gameObject);
    }
}