using System.Collections;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public static EnemyWaveController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private EnemyFactory enemyFactory;

    [SerializeField] private CardStatController[] cards;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float spawnArea = 5.0f;

    private int _waveCount;

    private int _waveSize;

    private void Start()
    {
        _waveSize  = Constants.ENEMY_WAVE_SIZE;
        _waveCount = _waveSize;
        
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (var i = 0; i < _waveSize; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var position = spawnPoint.position +
                           new Vector3(Random.Range(-spawnArea, spawnArea), 0, Random.Range(-spawnArea, spawnArea));
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
            var value = Random.Range(1, 5);

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
                GameManager.Instance.movementSpeed += _value / 100;
                break;
        }

        foreach (var t in cards)
        {
            t.gameObject.SetActive(false);
        }

        GameManager.SetCursor(false);
        Time.timeScale = 1;

        _waveSize++;
        _waveCount = _waveSize;
        
        StartCoroutine(SpawnWave());
    }
}