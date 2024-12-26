using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    public void SpawnEnemy(Vector3 _position)
    {
        var enemy      = Instantiate(enemyPrefab, _position, Quaternion.identity);
        var controller = enemy.GetComponent<EnemyController>();
        controller.beacon = beacon;
        
        var type = (EnemyController.EnemyType) Random.Range(0, (int) EnemyController.EnemyType.LENGTH);
        controller.EnemyProperties = type switch
        {
            EnemyController.EnemyType.NORMAL => new NormalEnemy(),
            EnemyController.EnemyType.FAST   => new FastEnemy(),
            EnemyController.EnemyType.TANK   => new TankEnemy(),
            EnemyController.EnemyType.DAGGER => new DaggerEnemy(),
            _                                => controller.EnemyProperties
        };
        
        Debug.Log($"Spawned {type} enemy");
    }
}
