using System.Collections;
using UnityEngine;
public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;

    [SerializeField] private CardStatController[] cards;
    
    [SerializeField] private Transform[] spawnPoints;
    
    [SerializeField] private float spawnArea = 5.0f;

    private int _waveCount;
    
    private void Start()
    {
        _waveCount = Constants.ENEMY_WAVE_SIZE;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (var i = 0; i < Constants.ENEMY_WAVE_SIZE; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var position = spawnPoint.position + new Vector3(Random.Range(-spawnArea, spawnArea), 0, Random.Range(-spawnArea, spawnArea));
            enemyFactory.SpawnEnemy(position);
            yield return new WaitForSeconds(Constants.ENEMY_SPAWN_RATE);
        }
    }
    
    public void EnemyDied()
    {
        // if all enemies are dead -> shuffle cards
        _waveCount--;
        if (_waveCount == 0)
        {
            ShuffleCards();
        }
    }
    
    private void ShuffleCards()
    {
        foreach (var t in cards)
        {
            var type  = (CardStatController.CardStatType)Random.Range(0, (int)CardStatController.CardStatType.LENGTH);
            var value = Random.Range(1, 10);

            t.Init(type, value);
            t.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        GameManager.SetCursor(true);
    }
    
    public void ApplyCard(CardStatController.CardStatType _type, float _value)
    {
        switch (_type)
        {
            case CardStatController.CardStatType.CRITICAL:
                GameManager.Instance.criticalChance += _value / 100;
                break;
            case CardStatController.CardStatType.ATTACK_SPEED:
                GameManager.Instance.fireRate -= _value / 100;
                break;
            case CardStatController.CardStatType.MOVEMENT_SPEED:
                GameManager.Instance.movementSpeed += _value;
                break;
            case CardStatController.CardStatType.BEACON:
                GameManager.Instance.beaconOverload += _value / 100;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }

        foreach (var t in cards)
        {
            t.gameObject.SetActive(false);
        }

        GameManager.SetCursor(false);
        Time.timeScale = 1;
        _waveCount = Constants.ENEMY_WAVE_SIZE;
        StartCoroutine(SpawnWave());
    }
}
