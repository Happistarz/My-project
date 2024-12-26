using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
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
    
    private EnemyFactory _enemyFactory;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    [SerializeField] private GameObject[] cards;

    [SerializeField] private TMP_Text timerText;


    public const int GameDuration = 20 * 60; // 20 minutes
    public       int GameTime { get; private set; } = 0;

    [Header("Player Stats")] [SerializeField]
    public float criticalChance = 0.1f;

    [SerializeField] public float fireRate = 0.6f;

    [SerializeField] public float movementSpeed = 5.0f;

    [SerializeField] public float beaconOverload = 0.1f;

    public float ReloadTime { get; private set; } = 1.0f;
    public float BoostSpeed { get; private set; } = 5.0f;
    public float Damage     { get; private set; } = 10.0f;
    public float SpawnRate  { get; set; }         = Constants.ENEMY_SPAWN_RATE;

    public float DamageToEnemy
    {
        get
        {
            var damage = Damage;
            if (Random.value < criticalChance)
            {
                damage *= Constants.CRITICAL_MULTIPLIER;
            }

            return damage;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _enemyFactory = GetComponent<EnemyFactory>();
        
        // StartCoroutine(SpawnEnemy());
        StartCoroutine(Timer());

        SetCursor(false);

        // ShuffleCards();
    }

    private IEnumerator SpawnEnemy()
    {
        var wait = new WaitForSeconds(Constants.ENEMY_SPAWN_RATE);
        while (true && GameTime < GameDuration)
        {
            var x = Random.Range(-10, 10);
            var z = Random.Range(-10, 10);

            _enemyFactory.SpawnEnemy(new Vector3(x, 1, z));
            yield return wait;
        }
    }

    private IEnumerator Timer()
    {
        var wait = new WaitForSeconds(1);
        while (GameTime < GameDuration)
        {
            GameTime++;
            timerText.text = $"{GameTime / 60:00}:{GameTime % 60:00}";
            yield return wait;
        }
    }

    private void ShuffleCards()
    {
        foreach (var t in cards)
        {
            var type  = (CardStatController.CardStatType)Random.Range(0, (int)CardStatController.CardStatType.LENGTH);
            var value = Random.Range(1, 10);

            t.GetComponent<CardStatController>().Init(type, value);
            t.SetActive(true);
            Time.timeScale = 0;
        }

        SetCursor(true);
    }

    public void ApplyCard(CardStatController.CardStatType _type, float _value)
    {
        switch (_type)
        {
            case CardStatController.CardStatType.CRITICAL:
                criticalChance += _value / 100;
                break;
            case CardStatController.CardStatType.ATTACK_SPEED:
                fireRate -= _value / 100;
                break;
            case CardStatController.CardStatType.MOVEMENT_SPEED:
                movementSpeed += _value;
                break;
            case CardStatController.CardStatType.BEACON:
                beaconOverload += _value / 100;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }

        foreach (var t in cards)
        {
            t.SetActive(false);
        }

        SetCursor(false);
        Time.timeScale = 1;
    }

    public static void SetCursor(bool _visible)
    {
        Cursor.visible   = _visible;
        Cursor.lockState = _visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}