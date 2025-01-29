using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    private readonly Dictionary<EnemyController.EnemyType, EnemyTypes> _enemyTypes =
        new()
        {
            { EnemyController.EnemyType.NORMAL, new NormalEnemy() },
            { EnemyController.EnemyType.FAST, new FastEnemy() },
            { EnemyController.EnemyType.TANK, new TankEnemy() },
            { EnemyController.EnemyType.DAGGER, new DaggerEnemy() }
        };

    public void SpawnEnemy(Vector3 _position)
    {
        var enemy      = Instantiate(enemyPrefab, _position, Quaternion.identity);
        var controller = enemy.GetComponent<EnemyController>();
        controller.beacon = beacon;

        var type = (EnemyController.EnemyType)Random.Range(0, (int)EnemyController.EnemyType.LENGTH);

        controller.EnemyProperties = _enemyTypes[type];
    }

    public void UpdateEnemiesStats()
    {
        foreach (var enemyType in _enemyTypes)
        {
            foreach (var attribute in enemyType.Value.Modifiers)
            {
                switch (attribute.Key)
                {
                    case "Health":
                        enemyType.Value.Health += attribute.Value;
                        break;
                    case "AttackDamage":
                        enemyType.Value.AttackDamage += (int)attribute.Value;
                        break;
                    case "AttackSpeed":
                        enemyType.Value.AttackSpeed += attribute.Value;
                        break;
                    case "Speed":
                        enemyType.Value.Speed += attribute.Value;
                        break;
                    case "Score":
                        enemyType.Value.Score += (int)attribute.Value;
                        break;
                }
            }
        }
    }
}