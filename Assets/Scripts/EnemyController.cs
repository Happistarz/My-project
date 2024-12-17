using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public Transform target;
    
    private const float Speed = 2.0f;

    // Update is called once per frame
    private void Update()
    {
        var direction = target.position - transform.position;
        direction.y = 0;
        transform.Translate(direction.normalized * Speed * Time.deltaTime);
    }
}
