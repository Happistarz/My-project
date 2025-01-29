using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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

    [SerializeField] private TMP_Text timerText;

    [SerializeField] private InputActionReference escapeAction;

    private const int _GAME_DURATION = 10 * 60;
    private       int GameTime { get; set; }

    [Header("Player Stats")]
    [SerializeField] public float criticalChance = 0.1f;

    [SerializeField] public float fireRate = 0.6f;

    [SerializeField] public float movementSpeed = 1.0f;

    public  float ReloadTime { get; private set; } = 1.0f;
    public  float BoostSpeed { get; private set; } = 0.5f;
    public float Damage     { get; set; }         = 10.0f;

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

    [SerializeField] private EnemyWaveController enemyWaveController;

    public int Score { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        StartCoroutine(Timer());

        SetCursor(false);

        escapeAction.action.Enable();
        escapeAction.action.performed += _ => OnEscape();
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

        SceneManager.LoadScene("GameOver");
    }

    public static void SetCursor(bool _visible)
    {
        Cursor.visible   = _visible;
        Cursor.lockState = CursorLockMode.None;
    }

    private static void OnEscape()
    {
        Application.Quit();
    }
}