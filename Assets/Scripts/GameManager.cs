using System.Collections;
using TMPro;
using UnityEngine;

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

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    [SerializeField] private GameObject[] cards;

    [SerializeField] private TMP_Text timerText;


    private const int _GAME_DURATION = 20 * 60; // 20 minutes
    private       int GameTime { get; set; }

    [Header("Player Stats")] [SerializeField]
    public float criticalChance = 0.1f;

    [SerializeField] public float fireRate = 0.6f;

    [SerializeField] public float movementSpeed = 5.0f;

    [SerializeField] public float beaconOverload = 0.1f;

    public float ReloadTime { get; private set; } = 1.0f;
    public float BoostSpeed { get; private set; } = 5.0f;
    public float Damage     { get; }              = 10.0f;

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

    public EnemyWaveController EnemyWaveController { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        EnemyWaveController = GetComponent<EnemyWaveController>();
        
        StartCoroutine(Timer());

        SetCursor(false);
    }

    private IEnumerator Timer()
    {
        var wait = new WaitForSeconds(1);
        while (GameTime < _GAME_DURATION)
        {
            GameTime++;
            timerText.text = $"{GameTime / 60:00}:{GameTime % 60:00}";
            yield return wait;
        }
    }

    public static void SetCursor(bool _visible)
    {
        Cursor.visible   = _visible;
        Cursor.lockState = _visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}