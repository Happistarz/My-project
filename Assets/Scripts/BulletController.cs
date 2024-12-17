using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed    = 10.0f;
    [SerializeField] private float lifeTime = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!_other.gameObject.CompareTag("Enemy")) return;
        
        Destroy(_other.transform.parent.gameObject);
        Destroy(gameObject);
    }
}